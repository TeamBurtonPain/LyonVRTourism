const base64Img = require('base64-img');
const crypto = require('crypto');
const Quest = require('../models/quest.js');
const { createServerError } = require('../../helpers/errors');

async function getAll() {
    return Quest.find();
}

async function getQuestById(questId) {
    return Quest.findById(questId);
}

async function createQuest(quest, picture) {
    const name = crypto.randomBytes(16).toString('hex');
    const filePath = await base64ToPromise(picture, 'pictures', name);
    if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
    quest.picturePath = filePath;
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

function base64ToPromise(imgData, dest, name) {
    return new Promise((resolve, reject) => {
        base64Img.img(imgData, dest, name, (err, response) => {
            if (err) reject(err);
            resolve(response);
        });
    });
}

module.exports = {
    getAll,
    getQuestById,
    createQuest,
    updateQuest,
    deleteQuest,
};
