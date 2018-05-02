const router = require('express').Router();
const am = require('../../helpers/async-middleware');
const questController = require('../controllers/quest-controller');
const roleGuard = require('../middlewares/role-guard');

router.get('/', am(questController.getAllQuests));
router.get('/:id', am(questController.getQuestById));

// Require authentification for the following routes
router.use(require('../middlewares/auth-guard'));
router.post('/', roleGuard('CREATOR'), am(questController.createQuest));
router.put('/:id', roleGuard('CREATOR'), am(questController.updateQuest));
router.delete('/:id', roleGuard('CREATOR'), am(questController.deleteQuest));

module.exports = router;
