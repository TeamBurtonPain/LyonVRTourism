const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const authController = require('../controllers/auth-controller');

router.post('/login', am(authController.createAccount));

module.exports = router;
