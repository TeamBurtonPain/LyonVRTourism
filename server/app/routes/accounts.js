const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const accountController = require('../controllers/account-controller');

router.post('/', am(accountController.createAccount));

// Require authentification for the following routes
// router.use(require('../middlewares/auth-guard'));
router.get('/:id', am(accountController.getAccountById));
router.put('/:id', am(accountController.updateAccount));
router.delete('/:id', am(accountController.deleteAccount));

// TODO: add ADMIN right update

module.exports = router;
