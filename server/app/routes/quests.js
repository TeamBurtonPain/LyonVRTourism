const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const questController = require('../controllers/quest-controller');

router.post('/', am(questController.createQuest));
router.get('/:id(\\d+)', am(questController.getQuestById));
router.put('/:id(\\d+)', am(questController.updateQuest));
router.delete('/:id(\\d+)', am(questController.deleteQuest));

module.exports = router;
