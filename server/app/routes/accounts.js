const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const accountController = require('../controllers/account-controller');
const roleGuard = require('../middlewares/role-guard');

router.post('/', am(accountController.createAccount));

// Require authentification for the following routes
router.use(require('../middlewares/auth-guard'));
router.get('/:id', am(accountController.getAccountById));
router.put('/:id', roleGuard('ME'), am(accountController.updateAccount));
router.delete('/:id', roleGuard('ME'), am(accountController.deleteAccount));

module.exports = router;
