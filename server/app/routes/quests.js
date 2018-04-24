const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const questController = require('../controllers/quest-controller');

router.get('/', am(questController.getAll));

module.exports = router;
