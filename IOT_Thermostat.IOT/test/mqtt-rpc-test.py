import paho.mqtt.client as mqtt
from time import sleep


def on_connect(client, userdata, flags, rc):
    """
    The callback for when the client receives a CONNACK response from the server.
    """
    print("Connected with result code "+str(rc))

    # Subscribing in on_connect() means that if we lose the connection and
    # reconnect then subscriptions will be renewed.
    client.subscribe("MQTTnet.RPC/#")


def on_message(client, userdata, msg):
    """
    The callback for when a PUBLISH message is received from the server.
    """
    print(msg.topic+" "+str(msg.payload))
    if(msg.topic.endswith("/response")):
        return
    sleep(2)
    client.publish(msg.topic+"/response", 25, 0, False)


client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message

client.connect("192.168.0.178", 1883, 60)

# Blocking call that processes network traffic, dispatches callbacks and
# handles reconnecting.
# Other loop*() functions are available that give a threaded interface and a
# manual interface.
client.loop_start()

while True:
    sleep(5)
