const router = require('express').Router();
const jwt = require('express-jwt');

const { secret } = require('../../config/jwt');
const { isTokenRevoked } = require('../services/account-service');


function isRevokedCallback(req, payload, done) {
    const tokenId = payload.jit;

    isTokenRevoked(tokenId)
        .then(jit => done(null, !!jit))
        .catch(err => done(err));
}

router.use(jwt({
    secret: secret,
    isRevoked: isRevokedCallback
}));

module.exports = router;
