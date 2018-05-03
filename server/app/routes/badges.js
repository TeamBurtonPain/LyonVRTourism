const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const badgeController = require('../controllers/badge-controller');

router.get('/', am(badgeController.getAllBadges));
router.get('/:id', am(badgeController.getBadgeById));

module.exports = router;
