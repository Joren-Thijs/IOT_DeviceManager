require('dotenv').config();
const fs = require('fs');
const exec = require('child_process').exec;
const async = require('async');

const scriptsFolder = './devices/';

const files = fs.readdirSync(scriptsFolder);
const funcs = files.map(function (file) {
    return exec.bind(null, `nodemon ${scriptsFolder}${file}`);
});

function getResults(err, data) {
    if (err) {
        return console.log(err);
    }
    const results = data.map(function (lines) {
        return lines.join('');
    });
    console.log(results);
}

async.parallel(funcs, getResults);
