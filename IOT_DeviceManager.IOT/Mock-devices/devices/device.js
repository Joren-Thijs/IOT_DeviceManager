//mock-device.js
var mqtt = require('mqtt');
var client = mqtt.connect('mqtt://127.0.0.1');
client.on('connect', function () {
    console.log('Connected!');
    setInterval(function () {
        client.publish('device/C3P0/ms', "{'status':{'onStatus': 'true' }}");
        console.log('Messages Sent');
    }, 5000);
});
