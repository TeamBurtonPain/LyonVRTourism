const passportJWT = require('passport-jwt');
const Account = require('../app/models/account');
const jwtConfig = require('./jwt');

const JwtStrategy = passportJWT.Strategy;
const ExtractJwt = passportJWT.ExtractJwt;

const jwtOptions = {};
jwtOptions.jwtFromRequest = ExtractJwt.fromAuthHeaderWithScheme('jwt');
jwtOptions.secretOrKey = jwtConfig.secret;

module.exports = new JwtStrategy(jwtOptions, ((jwtPayload, next) => {
    // eslint-disable-next-line
    console.log('Payload received', jwtPayload);

    Account.findOne({ 'connection.jwt': jwtPayload.id }, (err, account) => {
        if (account) {
            next(null, account);
        } else {
            next(null, false);
        }
    });
}));
