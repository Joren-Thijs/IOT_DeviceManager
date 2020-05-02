import paho.mqtt.client as mqtt
import devicename as deviceName
import json
from time import sleep


class MQTTClient:
    """
    This is an MQTT wrapper class for our thermostat.
    """

    ''' Class Variables '''
    status = True  # Thermostat ON/OFF status
    setpoint = 0  # Temperature setpoint
    temperature = 0  # Measured Temperature

    def __init__(self):
        # create mqtt client
        self._client = mqtt.Client(client_id=deviceName.getDeviceName(), clean_session=False,
                                   userdata=None, transport="tcp")
        self._client.on_connect = self.on_connect
        self._client.on_disconnect = self.on_disconnect
        self._client.on_message = self.on_message

        # connect to mqtt broker. connect(ip adress, port, keep alive time)
        self._client.connect("169.254.39.51", 1883, 60)

        # start listening for incoming messages
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
        self._client.subscribe("cmd/#")

    def on_disconnect(self, client, userdata, rc):
        if rc != 0:
            self.status = False

    def on_message(self, client, userdata, msg):
        """
        The callback for when a PUBLISH message is received from the server.

        :param client: the client instance for this callback
        :param userdata: the private user data as set in Client() or user_data_set()
        :param msg: an instance of MQTTMessage. This is a class with members topic, payload, qos, retain.
        """
        print(msg.topic+" "+str(msg.payload.decode('utf-8')))

        if msg.topic == "cmd/status":
            # Decode the bytes string into a unicode string
            payload = msg.payload.decode('utf-8')
            # Convert the string back to dictionary
            command = json.loads(payload)
            # Grab the status string from the payload dict
            self.status = command['status'] == 'True'

        if msg.topic == "cmd/setpoint":
            # Decode the bytes string into a unicode string
            payload = msg.payload.decode('utf-8')
            # Convert the string back to dictionary
            command = json.loads(payload)
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

    def sendMeasurement(self, temperature):
        data = {
            'temp': temperature,
            'sp': self.setpoint,
            'st': self.status
        }
        payload = json.dumps(data)
        self._client.publish("ms", payload, 0, False)
