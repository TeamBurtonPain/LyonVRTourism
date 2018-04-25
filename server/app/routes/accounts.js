const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const accountController = require('../controllers/account-controller');

router.post('/', am(accountController.createAccount));
router.get('/:id', am(accountController.getAccountById));
router.put('/:id', am(accountController.updateAccount));
router.delete('/:id', am(accountController.deleteAccount));

module.exports = router;
