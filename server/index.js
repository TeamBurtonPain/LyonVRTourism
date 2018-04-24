// Dependencies import
const express = require('express');
const morgan = require('morgan');
const chalk = require('chalk');
const bodyParser = require('body-parser');

// Config import
const WEB_CONFIG = require('./config/web.js');

// App import
const routes = require('./app/routes');

const app = express();

// Log requests with beautifull colors
app.use(morgan((tokens, req, res) => {
    return `${chalk.blue(tokens.method(req, res))} ${ chalk.green(tokens.url(req, res))} ${ chalk.red(tokens['response-time'](req, res))}`;
}));

//Parse the request to JSON
app.use(bodyParser.json());

// Serve the API first
app.use('/api/', routes);

// 404 Not found
app.use('*', (req, res) => {
    res.sendStatus(404);
});


app.listen(WEB_CONFIG.port, () => {
    // eslint-disable-next-line
    console.log(`App is running on port ${WEB_CONFIG.port}!`);
});
