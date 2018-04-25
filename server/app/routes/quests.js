const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const questController = require('../controllers/quest-controller');

router.get('/', am(questController.getAllQuests));
router.post('/', am(questController.createQuest));
router.get('/:id', am(questController.getQuestById));
router.put('/:id', am(questController.updateQuest));
router.delete('/:id', am(questController.deleteQuest));

module.exports = router;
