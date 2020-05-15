# Imports
from gpiozero import LED, Button
from time import sleep
import busio
import digitalio
import board
import adafruit_mcp3xxx.mcp3008 as MCP
from adafruit_mcp3xxx.analog_in import AnalogIn
from adafruit_ht16k33 import segments

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
chan0 = AnalogIn(mcp, MCP.P0)
# create an analog input channel on pin 1
chan1 = AnalogIn(mcp, MCP.P1)
####################################################################

# Seg ##############################################################
# Create the LED segment class.
# This creates a 7 segment 4 character display:
display1 = segments.Seg7x4(i2c, 0x70)
display2 = segments.Seg7x4(i2c, 0x71)

# Clear the display.
display1.fill(0)
display2.fill(0)
####################################################################

# Inputs
onButton = Button(23)
webButton = Button(24)

# Outputs
statusLED = LED(5)
heaterLED = LED(6)


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


# Main loop
while True:
    sleep(1)

    if not onButton.is_pressed:
        statusLED.off()
        heaterLED.off()
    else:
        statusLED.on()
        heaterLED.on()

    print('CH0 Value: ', chan0.value)
    print('CH0 Voltage: ' + str(chan0.voltage) + 'V')
    chan0Temp = remap_temp(chan0.value)
    print('CH0 Temp: ' + str(chan0Temp) + '℃')

    print('CH1 Value: ', chan1.value)
    print('CH1 Voltage: ' + str(chan1.voltage) + 'V')
    chan1Temp = remap_temp(chan1.value)
    print('CH1 Temp: ' + str(chan1Temp) + '℃')

    # Clear the display.
    display1.fill(0)
    display2.fill(0)

    # Can just print a number
    display1.print(chan0Temp)
    display2.print(chan1Temp)
