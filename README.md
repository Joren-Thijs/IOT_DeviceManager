## Project description

A device manager for IOT devices with applications in home automation and data collection.

## Technical overview

It uses the MQTT protocol to communicate with different IOT devices.
The measurement data and settings of these devices are stored in a SQL database.
This data can be retieved from an API written in ASP.NET Core 3.1.
The data can be viewed and the devices can be managed from a webinterface made with Angular 9.
This app is deployed using docker.

## Setup

1. Clone this repository
2. Open the folder using your favourite IDE (aka. _**VsCode**_)
3. Run `docker compose up` in the root of the cloned folder
4. Enjoy the magic
