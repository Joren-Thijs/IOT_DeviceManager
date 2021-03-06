import paho.mqtt.client as mqtt
import devicename as deviceName
import json
import threading
import settings
import asyncio
from time import sleep
from datetime import datetime


class MQTTClient:
    """
    This is an MQTT wrapper class for our thermostat.
    """

    ''' Class Variables '''
    status_lock = threading.RLock()
    _api_connection_lock = threading.RLock()
    _api_answer_received_lock = threading.RLock()
    _api_connection = True
    _status = False  # Thermostat ON/OFF status
    setpoint = 20  # Temperature setpoint
    temperature = 0  # Measured Temperature

    def __init__(self):
        self._deviceTopic = settings.DEVICE_TYPE + "/" + settings.DEVICE_NAME

        self.setup_mqtt_client()

        self.setup_connection_handlers()

        self.setup_message_callbacks()

        # connect to mqtt broker. connect(ip adress, port, keep alive time)
        self._client.connect_async(
            settings.MQTT_BROKER_NAME, settings.MQTT_BROKER_PORT, 30)

        self.startListening()

    def setup_mqtt_client(self):
        self._client = mqtt.Client(client_id=settings.DEVICE_NAME, clean_session=False,
                                   userdata=None, transport="tcp")
        self._client.will_set(
            self._deviceTopic + "/diconnected", payload="disconnected", qos=1, retain=False)

    def setup_connection_handlers(self):
        self._client.on_connect = self.on_connect
        self._client.on_disconnect = self.on_disconnect
        self._client.reconnect_delay_set(min_delay=5, max_delay=20)

    def setup_message_callbacks(self):
        self._client.message_callback_add(
            self._deviceTopic + "/request/ping/response", self.on_ping_message)
        self._client.message_callback_add(
            self._deviceTopic + "/cmd/status", self.on_status_message)

    def on_connect(self, client, userdata, flags, rc):
        """
        The callback for when the client receives a CONNACK response from the server.

        :param client: the client instance for this callback
        :param userdata: the private user data as set in Client() or user_data_set()
        :param flags: response flags sent by the broker
        :param rc: the connection result
        """

        print("Connected with result code " + str(rc))

        # Subscribing in on_connect() means that if we lose the connection and
        # reconnect then subscriptions will be renewed.
        self._client.subscribe(self._deviceTopic + "/request/ping/response")
        self._client.subscribe(self._deviceTopic + "/cmd/+")
        api_connection_handler_thread = threading.Thread(
            target=self.api_connection_handler_async)
        api_connection_handler_thread.start()

    def on_disconnect(self, client, userdata, rc):
        if rc != 0:
            print("disconnected unexpectedly")
            self.status_lock.acquire()
            self.status = False
            self.status_lock.release()

    def api_connection_handler_async(self):
        while True:
            self.ping_api()
            sleep(10)
            self.check_api_answer()
            self.check_api_connection()

    def ping_api(self):
        self._api_answer_received_lock.acquire()
        self._api_answer_received = False
        self._api_answer_received_lock.release()
        self._client.publish(self._deviceTopic + "/request/ping", "", 0, False)

    def on_ping_message(self, client, userdata, msg):
        self._api_answer_received_lock.acquire()
        self._api_answer_received = True
        self._api_answer_received_lock.release()

    def check_api_answer(self):
        self._api_connection_lock.acquire()
        self._api_answer_received_lock.acquire()
        self._api_connection = self._api_answer_received
        self._api_answer_received_lock.release()
        self._api_connection_lock.release()

    def check_api_connection(self):
        if not self._api_connection:
            print("Error: no connection to web api")
            self._status = False

    def on_status_message(self, client, userdata, msg):
        """
        The callback for when a PUBLISH message is received from the server.

        :param client: the client instance for this callback
        :param userdata: the private user data as set in Client() or user_data_set()
        :param msg: an instance of MQTTMessage. This is a class with members topic, payload, qos, retain.
        """

        # Decode the bytes string into a unicode string
        payload = msg.payload.decode('utf-8')
        # Convert the string back to dictionary
        try:
            command = json.loads(payload)
        except:
            print("error while decoding payload")
            return
        # Grab the status from the payload dict
        self.status_lock.acquire()
        self.status = command['OnStatus'] == True
        self.status_lock.release()
        self.setpoint = command['Settings']['setpoint']
        self.sendStatusResponse()

    def startListening(self):
        """
        Start listening for network traffic
        """
        # Blocking call that processes network traffic, dispatches callbacks and
        # handles reconnecting.
        self._client.loop_start()

    def stopListening(self):
        """
        Stop listening for network traffic
        """
        self._client.loop_stop()

    def sendMeasurement(self):
        data = {
            'Status':
            {
                'OnStatus': self.status,
                'Settings': {
                    'setpoint': self.setpoint
                }
            },
            'Values': {
                'temperature': self.temperature
            },
            'TimeStamp': datetime.now(tz=None)
        }
        try:
            payload = json.dumps(data, default=str)
        except:
            print("error while encoding payload")
            return

        self._client.publish(self._deviceTopic + "/ms", payload, 0, False)

    def sendStatusResponse(self):
        data = {
            'OnStatus': self.status,
            'Settings': {
                'setpoint': self.setpoint
            }
        }
        try:
            payload = json.dumps(data)
        except:
            print("error while encoding payload")
            return

        self._client.publish(self._deviceTopic +
                             "/cmd/status/response", payload, 0, False)

    @property
    def status(self):
        return self._status

    @status.setter
    def status(self, value):
        if not self._api_connection:
            self._status = False
        else:
            self._status = value
