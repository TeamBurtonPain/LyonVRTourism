const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const accountController = require('../controllers/account-controller');

router.get('/:id(\\d+)', am(accountController.getAccountById));

module.exports = router;
