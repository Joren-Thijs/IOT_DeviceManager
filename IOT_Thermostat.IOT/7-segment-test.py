import time
import board
import busio
from adafruit_ht16k33 import segments

# Create the I2C interface.
i2c = busio.I2C(board.SCL, board.SDA)

# Create the LED segment class.
# This creates a 7 segment 4 character display:
display1 = segments.Seg7x4(i2c, 0x70)
display2 = segments.Seg7x4(i2c, 0x71)

# Clear the display.
display1.fill(0)
display2.fill(0)

i = 0

while True:
    # Clear the display.
    display1.fill(0)
    display2.fill(0)

    # Can just print a number
    display1.print(i)
    display2.print(i+1)
    i += 1
    time.sleep(0.5)
