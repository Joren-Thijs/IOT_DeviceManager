''' Imports '''

from time import sleep
# GPIO
from gpiozero import LED, Button
import busio
import digitalio
import board
# ADC
import adafruit_mcp3xxx.mcp3008 as MCP
from adafruit_mcp3xxx.analog_in import AnalogIn
# 7 Segment displays
from adafruit_ht16k33 import segments
# mqtt client
from mqtt_client import MQTTClient

''' Global Variables '''
sleepTime = 1  # Sleep Time in seconds
setpoint = 0  # Temperature setpoint
temperature = 0  # Measured Temperature
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


def startup():

    measure_temperature()

    measure_setpoint()

    # Set values to mqqt client
    mqtt.temperature = temperature
    mqtt.setpoint = setpoint
    mqtt.status = False

    turn_off_leds()

    display_off_status()

    display_temperature()


def measure_temperature():
    temperature = remap_temp(tempSensor.value)


def measure_setpoint():
    setpoint = remap_temp(manualSetpoint.value)


def turn_off_leds():
    statusLED.off()
    heater.off()


def clear_displays():
    setpointDisplay.fill(0)
    tempDisplay.fill(0)


def display_off_status():
    setpointDisplay.fill(0)
    setpointDisplay.print("0ff")


def display_temperature():
    tempDisplay.fill(0)
    tempDisplay.print(temperature)


def display_setpoint():
    setpointDisplay.fill(0)
    setpointDisplay.print(setpoint)


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


''' Initialization '''

initialize()
startup()

''' Main Loop '''
while True:
    # Sleep to unburden the CPU
    sleep(sleepTime)

    measure_temperature()

    # Check if the heater is ON/OFF
    if not onButton.is_pressed or onButton.is_pressed and webButton.is_pressed and not mqtt.status:
        statusLED.off()
        heater.off()
        # Clear The dispalys
        setpointDisplay.fill(0)
        tempDisplay.fill(0)
        # Display the off status
        setpointDisplay.print("0ff")
        # Display the current Temperature
        tempDisplay.print(temperature)
        continue

    # If the thermostat is on
    statusLED.on()

    # Check if we are using the web controller to control the setpoint
    if not webButton.is_pressed:
        mqtt.status = True
        measure_setpoint()
        mqtt.setpoint = setpoint
    else:
        setpoint = mqtt.setpoint

    # Check if temperature is to far below the setpoint and turn on the heater
    if temperature < setpoint - temperatureTolerance:
        heater.on()

    # Check if temperature is to far above the setpoint and turn off the heater
    if temperature > setpoint + temperatureTolerance:
        heater.off()

    display_setpoint()

    display_temperature()

    # Send Measurement Data
    if webButton.is_pressed:
        mqtt.sendMeasurement(temperature)
