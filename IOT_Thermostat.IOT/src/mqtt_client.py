import paho.mqtt.client as mqtt
from time import sleep


class MQTTClient:
    """
    This is MQTT wrapper class for our thermostat.
    """

    ''' Class Variables '''
    deviceName = ""
    setpoint = 0  # Temperature setpoint
    temperature = 0  # Measured Temperature

    def __init__(self):
        # set device name
        self.deviceName = self.getDeviceName()

        # create mqtt client
        self._client = mqtt.Client(client_id=self.deviceName, clean_session=False,
                                   userdata=None, transport="tcp")
        self._client.on_connect = self.on_connect
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

        print("Connected with result code "+str(rc))

        # Subscribing in on_connect() means that if we lose the connection and
        # reconnect then subscriptions will be renewed.
        self._client.subscribe("#")

    def on_message(self, client, userdata, msg):
        """
        The callback for when a PUBLISH message is received from the server.

        :param client: the client instance for this callback
        :param userdata: the private user data as set in Client() or user_data_set()
        :param msg: an instance of MQTTMessage. This is a class with members topic, payload, qos, retain.
        """
        print(msg.topic+" "+str(msg.payload))

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
        self._client.publish("/measurement/temperature", temperature, 0, False)

    def getDeviceName(self):
        ethName = self.getEthName()
        ethMAC = self.getMAC(ethName)
        return ethMAC

    def getEthName(self):
        # Get name of the Ethernet interface
        try:
            for root, dirs, files in os.walk('/sys/class/net'):
                for dir in dirs:
                    if dir[:3] == 'enx' or dir[:3] == 'eth':
                        interface = dir
        except:
            interface = "None"
        return interface

    def getMAC(self, interface='eth0'):
        # Return the MAC address of the specified interface
        try:
            str = open('/sys/class/net/%s/address' % interface).read()
        except:
            str = "00:00:00:00:00:00"
        return str[0:17]
