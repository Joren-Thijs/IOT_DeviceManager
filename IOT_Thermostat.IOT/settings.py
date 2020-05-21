import os
from dotenv import load_dotenv
load_dotenv()

DEVICE_TYPE = 0s.getenv("DEVICE_TYPE")
DEVICE_NAME = os.getenv("DEVICE_NAME")
MQTT_BROKER_NAME = os.getenv("MQTT_BROKER_NAME")
MQTT_BROKER_PORT = int(os.getenv("MQTT_BROKER_PORT"))
MQTT_USERNAME = os.getenv("MQTT_USERNAME")
MQTT_PASSWORD = os.getenv("MQTT_PASSWORD")
