# Imports
from gpiozero import LED, Button
from time import sleep

# Inputs
onButton = Button(23)
webButton = Button(24)

# Outputs
statusLED = LED(5)
heaterLED = LED(6)

# Main loop
while True:
    sleep(0.5)

    if not onButton.is_pressed:
        statusLED.off()
        heaterLED.off()
    else:
        statusLED.on()
        heaterLED.on()

    if webButton.is_pressed:
        # Read setpoint
        print('Web API is enabled')

        # Read temperature
