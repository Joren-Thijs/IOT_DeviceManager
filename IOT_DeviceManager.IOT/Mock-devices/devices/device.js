//mock-device.js
var mqtt = require('mqtt');
var client = mqtt.connect('mqtt://127.0.0.1');

var deviceType = 'mock-device';
var deviceId = 'C3P0';
var deviceTopicString = deviceType + '/' + deviceId;
var onStatus = false;

client.on('connect', function () {
    client.subscribe(deviceTopicString + '/cmd/+');
    setInterval(sendMeasurement, 3000);
});

function sendMeasurement() {
    let message = {
        Status: {
            OnStatus: onStatus,
            Settings: {
                referenceVoltage: 5,
                operatingMode: 'auto',
            },
        },
        Values: {
            sensor_1: getRandomInt(15, 25),
            sensor_2: getRandomInt(15, 25),
            sensor_3: getRandomInt(15, 25),
        },
        TimeStamp: new Date(),
    };
    let payload = JSON.stringify(message);
    client.publish(deviceTopicString + '/ms', payload);
}

client.on('message', function (topic, message) {
    context = topic.toString() + ' : ' + message.toString();
    console.log(context);

    if (topic.endsWith('cmd/status')) {
        let payload = JSON.parse(message);
        onStatus = payload.OnStatus;
        sendStatusMessage();
    }
});

function sendStatusMessage() {
    let message = {
        OnStatus: onStatus,
        Settings: {
            referenceVoltage: 5,
            operatingMode: 'auto',
        },
    };
    let payload = JSON.stringify(message);
    client.publish(deviceTopicString + '/cmd/status/response', payload);
}

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}
