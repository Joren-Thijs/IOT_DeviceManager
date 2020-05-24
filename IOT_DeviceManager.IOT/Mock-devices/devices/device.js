//mock-device.js
var mqtt = require('mqtt');
var client = mqtt.connect('mqtt://127.0.0.1');

var deviceType = 'mock-device';
var deviceId = 'C3P0';
var mqqtMessageTopic = deviceType + '/' + deviceId + '/ms';

var message = {
    Status: {
        OnStatus: true,
        Settings: {
            referenceVoltage: 5,
            operatingMode: 'auto',
        },
    },
    Values: {
        sensor_1: 20,
        sensor_2: 25,
        sensor_3: 23.65,
    },
    TimeStamp: new Date(),
};

var payload = JSON.stringify(message);

client.on('connect', function () {
    console.log('Connected!');
    setInterval(function () {
        client.publish(mqqtMessageTopic, payload);
        console.log('Messages Sent');
    }, 5000);
});
