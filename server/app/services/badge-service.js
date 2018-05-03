const base64Img = require('base64-img');
const uuidv4 = require('uuid-v4');
const Badge = require('../models/badge.js');
const { createUserError, createServerError } = require('../../helpers/errors');

async function getAll() {
    const badges = await Badge.find().lean();
    badges.map(badge => {
        badge.picture = null;
        delete badge.picturePath;
        return badge;
    });
    return badges;
}

async function getBadgeById(badgeId) {
    let badge = await Badge.findById(badgeId);
    if (!badge) throw createUserError('Unknow badge', 'No badge found with the provided _id.');
    badge = badge.toJSON();
    badge.picture = await base64EncodeToPromise(badge.picturePath);
    delete badge.picturePath;
    return badge;
}


async function createBadge(badge, picture) {
    const name = uuidv4();
    const filePath = await base64DecodeToPromise(picture, 'pictures', name);
    if (!filePath) throw createServerError('ServerError', 'Picture decode/write failed.');
    badge.picturePath = filePath;
    return badge.save();
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
    createBadge,
    getBadgeById
};
