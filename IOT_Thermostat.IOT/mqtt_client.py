import paho.mqtt.client as mqtt
import devicename as deviceName
import json
import threading
import settings
import asyncio
from time import sleep


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
        # create mqtt client
        self._client = mqtt.Client(client_id=settings.DEVICE_NAME, clean_session=False,
                                   userdata=None, transport="tcp")
        self._client.will_set(
            "diconnect", payload="disconnected", qos=1, retain=False)

        self._client.on_connect = self.on_connect
        self._client.on_disconnect = self.on_disconnect
        self._client.reconnect_delay_set(min_delay=5, max_delay=20)

        self._client.message_callback_add(
            settings.DEVICE_NAME + "/ping/response", self.on_ping_message)
        self._client.message_callback_add(
            settings.DEVICE_NAME + "/cmd/status", self.on_status_message)
        self._client.message_callback_add(
            settings.DEVICE_NAME + "/cmd/setpoint", self.on_setpoint_message)

        # connect to mqtt broker. connect(ip adress, port, keep alive time)
        self._client.connect_async(
            settings.MQTT_BROKER_NAME, settings.MQTT_BROKER_PORT, 30)

        self.startListening()

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
        self._client.subscribe(settings.DEVICE_NAME + "/ping/response")
        self._client.subscribe(settings.DEVICE_NAME + "/cmd/+")
        print("starting thread creation")
        thread = threading.Thread(target=self.api_connection_handler_async)
        thread.start()
        print("Connection handler finished")

    def on_disconnect(self, client, userdata, rc):
        if rc != 0:
            print("disconnected unexpectedly")
            self.status_lock.acquire()
            self.status = False
            self.status_lock.release()

    def api_connection_handler_async(self):
        print("api_connection_handler_async started")
        while True:
            self.ping_api()
            sleep(10)
            self.check_api_answer()
            self.check_api_connection()

    def ping_api(self):
        self._api_answer_received_lock.acquire()
        self._api_answer_received = False
        self._api_answer_received_lock.release()
        self._client.publish(settings.DEVICE_NAME + "/ping", "", 0, False)

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
            print("Lost connection to web api")
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
        # Grab the status string from the payload dict
        self.status_lock.acquire()
        self.status = command['status'] == 'True' or command['status'] == 'true'
        self.status_lock.release()
        self.sendStatusResponse()

    def on_setpoint_message(self, client, userdata, msg):
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
        # Grab the setpoint string from the payload dict
        self.setpoint = command['setpoint']

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
            'temperature': self.temperature,
            'setpoint': self.setpoint,
            'deviceStatus':
                {
                    'onStatus': self.status
                }
        }
        try:
            payload = json.dumps(data)
        except:
            print("error while encoding payload")
            return

        self._client.publish(settings.DEVICE_NAME + "/ms", payload, 0, False)

    def sendStatusResponse(self):
        data = {
            'temperature': self.temperature,
            'setpoint': self.setpoint,
            'deviceStatus':
                {
                    'onStatus': self.status
                }
        }
        try:
            payload = json.dumps(data)
        except:
            print("error while encoding payload")
            return

        self._client.publish(settings.DEVICE_NAME +
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
