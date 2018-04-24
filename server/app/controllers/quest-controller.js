// TODO: const { createUserError } = require('../../utils');
const questService = require('../services/quest-service.js');

async function getAll(req, res) {
    const quests = await questService.getAll();

    res.json(quests);
}

module.exports = {
    getAll,
};
