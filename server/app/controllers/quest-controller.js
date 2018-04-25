// TODO: const { createUserError } = require('../../utils');
const questService = require('../services/quest-service.js');
const Quest = require('../models/quest');

async function getAllQuests(req, res) {
    const quests = await questService.getAll();

    res.json(quests);
}

async function getQuestById(req, res) {
    const quest = await questService.getQuestById(req.params.id);

    res.json(quest);
}

async function createQuest(req, res) {
    let newQuest = new Quest(req.body);

    newQuest = await questService.createQuest(newQuest);

    res.json(newQuest);
}

async function updateQuest(req, res) {
    const newQuest = await questService.updateQuest(req.params.id, req.body);

    res.json(newQuest);
}

async function deleteQuest(req, res) {
    await questService.deleteQuest(req.params.id);

    res.sendStatus(200);
}


module.exports = {
    getAllQuests,
    getQuestById,
    createQuest,
    updateQuest,
    deleteQuest
};
