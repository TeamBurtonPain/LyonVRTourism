const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const accountController = require('../controllers/account-controller');

router.post('/', am(accountController.createAccount));
router.get('/:id(\\d+)', am(accountController.getAccountById));
router.put('/:id(\\d+)', am(accountController.updateAccount));
router.delete('/:id(\\d+)', am(accountController.deleteAccount));

module.exports = router;
