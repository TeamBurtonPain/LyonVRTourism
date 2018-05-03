// Dependencies import
const express = require('express');
const morgan = require('morgan');
const chalk = require('chalk');
const bodyParser = require('body-parser');
const mongoose = require('mongoose');

// Config import
const WEB_CONFIG = require('./config/web.js');
const DB_CONFIG = require('./config/db.js');

// App import
const routes = require('./app/routes');

const app = express();

// Configue custom morgan token

// eslint-disable-next-line
morgan.token('ip', (req, res) => {
    return req.connection.remoteAddress;
});

// Log requests with beautifull colors
app.use(morgan((tokens, req, res) => {
    var status = tokens.status(req, res);
    var statusColor = status >= 500 ?
        'red' : status >= 400 ?
            'yellow' : status >= 300 ?
                'cyan' : 'green';

    return `${chalk[statusColor](padRight(`${tokens.method(req, res) } ${ tokens.url(req, res)}`, 60))
    } ${ chalk[statusColor](status)
    } ${ chalk.blue(padLeft(tokens['remote-addr'](req, res) || '-', 8))
    } ${ chalk.green('-')
    } ${ chalk.green(tokens.res(req, res, 'content-length') || '-')
    } ${ chalk.yellow(`${tokens['response-time'](req, res) } ms`)}`;
}));

function padLeft(str, len) {
    return len > str.length ?
        (new Array(len - str.length + 1)).join(' ') + str :
        str;
}
function padRight(str, len) {
    return len > str.length ?
        str + (new Array(len - str.length + 1)).join(' ') :
        str;
}

//Parse the request to JSON
app.use(bodyParser.json({ limit: '10mb' }));

// Serve the API
app.use('/api/', routes);

// 404 Not found
app.use('*', (req, res) => {
    res.sendStatus(404);
});

// Connect database
mongoose.connect(DB_CONFIG.host);
const db = mongoose.connection;

db.once('open', () => {
    app.listen(WEB_CONFIG.port);
    // eslint-disable-next-line
    console.log('UrbanQuest started on port ' + WEB_CONFIG.port);
});
