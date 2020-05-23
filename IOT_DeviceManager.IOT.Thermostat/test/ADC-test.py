# Imports
from gpiozero import LED, Button
from time import sleep
import busio
import digitalio
import board
import adafruit_mcp3xxx.mcp3008 as MCP
from adafruit_mcp3xxx.analog_in import AnalogIn

# create the spi bus
spi = busio.SPI(clock=board.SCK, MISO=board.MISO, MOSI=board.MOSI)

# create the cs (chip select)
cs = digitalio.DigitalInOut(board.D8)

# create the mcp object
mcp = MCP.MCP3008(spi, cs)

# create an analog input channel on pin 0
chan0 = AnalogIn(mcp, MCP.P0)
# create an analog input channel on pin 1
chan1 = AnalogIn(mcp, MCP.P1)

# Inputs
onButton = Button(23)
webButton = Button(24)

# Outputs
statusLED = LED(5)
heaterLED = LED(6)

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

    print('CH1 Value: ', chan1.value)
    print('CH1 Voltage: ' + str(chan1.voltage) + 'V')
