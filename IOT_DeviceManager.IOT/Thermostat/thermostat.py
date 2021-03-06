''' Imports '''

from mqtt_client import MQTTClient
from adafruit_ht16k33 import segments
from adafruit_mcp3xxx.analog_in import AnalogIn
import adafruit_mcp3xxx.mcp3008 as MCP
import board
import digitalio
import busio
from gpiozero import LED, Button
from time import sleep


''' Global Variables '''
sleepTime = 2  # Sleep Time in seconds
temperatureTolerance = 2  # Temperature Hysteris Area

''' Functions '''


def initialize():
    # SPI Bus ##########################################################
    # create the spi bus
    spi = busio.SPI(clock=board.SCK, MISO=board.MISO, MOSI=board.MOSI)

    # create the cs (chip select)
    cs = digitalio.DigitalInOut(board.D8)
    ####################################################################

    # I2C Bus ##########################################################
    # Create the I2C interface.
    i2c = busio.I2C(board.SCL, board.SDA)
    ####################################################################

    # ADC ##############################################################
    # create the mcp object
    mcp = MCP.MCP3008(spi, cs)

    # create an analog input channel on pin 0
    global manualSetpoint
    manualSetpoint = AnalogIn(mcp, MCP.P0)
    # create an analog input channel on pin 1
    global tempSensor
    tempSensor = AnalogIn(mcp, MCP.P1)
    ####################################################################

    # 7 Segment displays ###############################################
    # This creates a 7 segment 4 character display:
    global setpointDisplay
    setpointDisplay = segments.Seg7x4(i2c, 0x70)
    global tempDisplay
    tempDisplay = segments.Seg7x4(i2c, 0x71)

    # Clear the display.
    setpointDisplay.fill(0)
    tempDisplay.fill(0)
    ####################################################################

    # Inputs ###########################################################
    global onButton
    onButton = Button(23)
    global webButton
    webButton = Button(24)
    ####################################################################

    # Outputs ##########################################################
    global statusLED
    statusLED = LED(5)
    global heater
    heater = LED(6)
    ####################################################################

    # MQTT Client ######################################################
    # Create the MQTT client
    global mqtt
    mqtt = MQTTClient()
    ####################################################################


def measure_temperature():
    mqtt.temperature = remap_temp(tempSensor.value)


def measure_setpoint():
    if not is_using_web():
        mqtt.setpoint = remap_temp(manualSetpoint.value)


def turn_off_leds():
    statusLED.off()
    heater.off()


def clear_displays():
    setpointDisplay.fill(0)
    tempDisplay.fill(0)


def display_off_status():
    turn_off_leds()
    clear_displays()
    setpointDisplay.print("0ff")
    display_temperature()


def display_on_status():
    statusLED.on()
    display_temperature()
    display_setpoint()


def display_status():
    if is_on():
        display_on_status()
    else:
        display_off_status()


def display_temperature():
    tempDisplay.fill(0)
    tempDisplay.print(mqtt.temperature)


def display_setpoint():
    setpointDisplay.fill(0)
    setpointDisplay.print(mqtt.setpoint)


def control_heater():
    if not is_on():
        return

    # Check if temperature is to far below the setpoint and turn on the heater
    if mqtt.temperature < mqtt.setpoint - temperatureTolerance:
        heater.on()

    # Check if temperature is to far above the setpoint and turn off the heater
    if mqtt.temperature > mqtt.setpoint + temperatureTolerance:
        heater.off()


def remap_range(value, left_min, left_max, right_min, right_max):
    # this remaps a value from original (left) range to new (right) range
    # Figure out how 'wide' each range is
    left_span = left_max - left_min
    right_span = right_max - right_min

    # Convert the left range into a 0-1 range (int)
    valueScaled = int(value - left_min) / int(left_span)

    # Convert the 0-1 range into a value in the right range.
    return int(right_min + (valueScaled * right_span))


def remap_temp(value):
    return remap_range(value, 0, 65535, -5, 40)


def is_using_web():
    return webButton.is_pressed


def is_on():
    mqtt.status_lock.acquire()
    on = onButton.is_pressed and not is_using_web()
    on_web = is_using_web() and mqtt.status
    mqtt.status = on or on_web
    mqtt.status_lock.release()
    return mqtt.status


def send_measurements():
    mqtt.sendMeasurement()


''' Initialization '''

initialize()

''' Main Loop '''
while True:
    # Sleep to unburden the CPU
    sleep(sleepTime)

    measure_temperature()

    measure_setpoint()

    display_status()

    control_heater()

    send_measurements()
