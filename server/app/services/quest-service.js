const Quest = require('../models/quest.js');

async function getAll() {
    return Quest.find();
}

async function getQuestById(questId) {
    return Quest.findById(questId);
}

async function createQuest(quest) {
    quest.picturePath = 'picturePath'; // TODO: implement image upload
    return quest.save();
}

async function updateQuest(questId, questModif) {
    const actualQuest = await Quest.findById(questId);
    actualQuest.merge(questModif);
    return actualQuest.save();
}

async function deleteQuest(questId) {
    const condition = { _id: questId };
    return Quest.remove(condition);
}

module.exports = {
    getAll,
    getQuestById,
    createQuest,
    updateQuest,
    deleteQuest,
};
