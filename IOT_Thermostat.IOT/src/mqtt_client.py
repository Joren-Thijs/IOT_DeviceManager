import paho.mqtt.client as mqtt
from time import sleep


class MQTTClient:
    def __init__(self):
        # create mqtt client
        self.client = mqtt.Client()
        self.client.on_connect = self.on_connect
        self.client.on_message = self.on_message

        # connect to mqtt broker. connect(ip adress, port, keep alive time)
        self.client.connect("169.254.39.51", 1883, 60)

    def on_connect(self, client, userdata, flags, rc):
        """
        The callback for when the client receives a CONNACK response from the server.
        :param client:
        """

        print("Connected with result code "+str(rc))

        # Subscribing in on_connect() means that if we lose the connection and
        # reconnect then subscriptions will be renewed.
        self.client.subscribe("#")

    def on_message(self, client, userdata, msg):
        """
        The callback for when a PUBLISH message is received from the server.
        """
        print(msg.topic+" "+str(msg.payload))
