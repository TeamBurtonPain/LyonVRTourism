const base64Img = require('base64-img');
const uuidv4 = require('uuid-v4');
const fs = require('fs');
const Quest = require('../models/quest.js');
const { createUserError, createServerError } = require('../../helpers/errors');

async function getAll() {
    const quests = await Quest.find({ open: true }).lean();
    quests.map(quest => {
        quest.checkpoints.map(checkpoint => {
            checkpoint.picture = null;
            return checkpoint;
        });
        return quest;
    });
    return quests;
}

async function getQuestById(questId) {
    let quest = await Quest.findById(questId);
    if (!quest) throw createUserError('Unknow quest', 'No quest found with the provided _id.');
    quest = quest.toJSON();
    for (const checkpoint of quest.checkpoints) {
        quest.checkpoints[quest.checkpoints.indexOf(checkpoint)].picture = await base64EncodeToPromise(checkpoint.picturePath);
        delete quest.checkpoints[quest.checkpoints.indexOf(checkpoint)].picturePath;
    }
    return quest;
}

async function createQuest(quest, checkpoints) {
    // Checkpoint pictures
    for (const checkpoint of checkpoints) {
        const name = uuidv4();
        const filePath = await base64DecodeToPromise(checkpoint.picture, 'pictures', name);
        if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
        quest.checkpoints[checkpoints.indexOf(checkpoint)].picturePath = filePath;
        quest.checkpoints[checkpoints.indexOf(checkpoint)].pictureName = name;
        delete checkpoint.picture;
    }

    return quest.save();
}

async function updateQuest(questId, questModif) {
    const actualQuest = await Quest.findById(questId);

    if (questModif.checkpoints) {
        const oldCheckpointsPicturePath = actualQuest.checkpoints.map(checkpoint => checkpoint.picturePath);
        for (const checkpoint of questModif.checkpoints) {
            const name = uuidv4();
            const filePath = await base64DecodeToPromise(checkpoint.picture, 'pictures', name);
            if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
            actualQuest.checkpoints[questModif.checkpoints.indexOf(checkpoint)].picturePath = filePath;
            questModif.checkpoints.checkpoints[questModif.checkpoints.indexOf(checkpoint)].pictureName = name;
        }
        for (const picturePath of oldCheckpointsPicturePath) {
            await new Promise(resolve => fs.unlink(picturePath, res => resolve(res)));
        }
    }

    actualQuest.merge(questModif);

    return actualQuest.save();
}

async function deleteQuest(questId) {
    const quest = await Quest.findById(questId);

    for (const picturePath of quest.checkpoints.map(checkpoint => checkpoint.picturePath)) {
        await new Promise(resolve => fs.unlink(picturePath, res => resolve(res)));
    }

    const condition = { _id: questId };

    return Quest.remove(condition);
}

function base64DecodeToPromise(imgData, dest, name) {
    return new Promise((resolve, reject) => {
        base64Img.img(imgData, dest, name, (err, response) => {
            if (err) reject(err);
            resolve(response);
        });
    });
}

function base64EncodeToPromise(path) {
    return new Promise((resolve, reject) => {
        base64Img.base64(path, (err, response) => {
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
