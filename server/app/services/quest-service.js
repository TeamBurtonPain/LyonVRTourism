const Quest = require('../models/quest.js');

async function getQuestById(questId) {
    return Quest.findById(questId);
}

async function createQuest(quest) {
    return quest.save();
}

async function updateQuest(quest) {
    const condition = { _id: quest._id };
    return Quest.update(condition, quest);
}

async function deleteQuest(questId) {
    const condition = { _id: questId };
    return Quest.remove(condition);
}

module.exports = {
    getQuestById,
    createQuest,
    updateQuest,
    deleteQuest,
};
