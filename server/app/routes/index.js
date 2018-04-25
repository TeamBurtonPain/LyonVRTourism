const router = require('express').Router();

router.use('/accounts', require('./accounts'));
router.use('/quests', require('./quests'));

module.exports = router;
