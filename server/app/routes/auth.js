const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const authController = require('../controllers/auth-controller');

router.post('/login', am(authController.login));

// Require authentification for the following routes
router.use(require('../middlewares/auth-guard'));
router.get('/logout', am(authController.logout));

module.exports = router;
