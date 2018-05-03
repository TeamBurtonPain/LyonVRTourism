const badgeService = require('../services/badge-service.js');

async function getAllBadges(req, res) {
    const quests = await badgeService.getAll();

    res.json(quests);
}

async function getBadgeById(req, res) {
    const quest = await badgeService.getBadgeById(req.params.id);

    res.json(quest);
}

module.exports = {
    getAllBadges,
    getBadgeById,
};
