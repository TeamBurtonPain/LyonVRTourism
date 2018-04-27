const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const questController = require('../controllers/quest-controller');

router.get('/', am(questController.getAllQuests));
router.get('/:id', am(questController.getQuestById));

// Require authentification for the following routes
router.use(require('../middlewares/auth-guard'));
router.post('/', am(questController.createQuest));
router.put('/:id', am(questController.updateQuest));
router.delete('/:id', am(questController.deleteQuest));

module.exports = router;
