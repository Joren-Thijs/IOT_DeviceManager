//mock-device.js
const { v4: uuidV4 } = require('uuid');
var mqtt = require('mqtt');
var client = mqtt.connect(process.env.MQTT_BROKER_NAME, {
    port: process.env.MQTT_BROKER_PORT,
    protocol: 'mqtt',
});
var deviceIdRequestDto;
var deviceType = process.env.DEVICE_TYPE;
var deviceId = 'no-id';
var deviceTopicString = deviceType + '/' + deviceId;
var onStatus = false;

client.on('connect', function () {
    if (deviceId == 'no-id') {
        requestDeviceId();
    } else {
        finishClientSetup();
    }
});

function finishClientSetup() {
    reloadDevicetopicString();
    setInterval(sendMeasurement, 3000);
    client.subscribe(deviceTopicString + '/cmd/+');
}

function requestDeviceId() {
    client.subscribe(deviceTopicString + '/request/id/response');
    deviceIdRequestDto = {
        DeviceType: deviceType,
        TransactionKey: uuidV4(),
    };
    let payload = JSON.stringify(deviceIdRequestDto);
    client.publish(deviceTopicString + '/request/id', payload);
}

function handleNewDeviceIdReceived(deviceIdRequestResponseDto) {
    if (isDeviceIdRequestResponseDtoCorrect(deviceIdRequestResponseDto)) {
        client.unsubscribe(deviceTopicString + '/request/id/response');
        deviceId = deviceIdRequestResponseDto.DeviceId;
    }
    finishClientSetup();
}

function isDeviceIdRequestResponseDtoCorrect(deviceIdRequestResponseDto) {
    return (
        deviceIdRequestDto.DeviceType ==
            deviceIdRequestResponseDto.DeviceType &&
        deviceIdRequestDto.TransactionKey ==
            deviceIdRequestResponseDto.TransactionKey
    );
}

function reloadDevicetopicString() {
    deviceTopicString = deviceType + '/' + deviceId;
}

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
    };
    let payload = JSON.stringify(message);
    client.publish(deviceTopicString + '/ms', payload);
}

client.on('message', function (topic, payload) {
    let message = JSON.parse(payload);

    if (topic.endsWith('request/id/response') && deviceId == 'no-id') {
        handleNewDeviceIdReceived(message);
    }

    if (topic.endsWith('cmd/status')) {
        onStatus = message.OnStatus;

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
