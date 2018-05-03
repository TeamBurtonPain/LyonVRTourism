const mongoose = require('mongoose');
// Models
const Account = require('../app/models/account');
const Quest = require('../app/models/quest');
const Badge = require('../app/models/badge');
const DB_CONFIG = require('../config/db.js');

// Data
const accountsData = require('./data/accounts');
const questData = require('./data/quest');
const badgesData = require('./data/badges');

mongoose.connect(DB_CONFIG.host);
const db = mongoose.connection;

// eslint-disable-next-line
console.log('UrbanQuest populate script :)');
// eslint-disable-next-line
console.log('Database connection...');

db.once('open', async () => {
    // eslint-disable-next-line
    console.log('Connected.');

    /*  DROP */
    // eslint-disable-next-line
    console.log('Drop existing documents...');
    await Account.remove({});
    await Quest.remove({});
    await Badge.remove({});
    // eslint-disable-next-line
    console.log('Dropped.');

    /*  PERSIST */
    // eslint-disable-next-line
    console.log('Data generation ...');
    for (const accountData of accountsData) {
        await Account.create(accountData);
    }
    for (const badgeData of badgesData) {
        await Badge.create(badgeData);
    }
    const quest = await Quest.create(new Quest(questData));
    // eslint-disable-next-line
    console.log('Data generated.');

    /*  ASSOCIATE */
    // eslint-disable-next-line
    console.log('Data association ...');
    for (const checkpoint of quest.checkpoints) {
        let badge;
        switch (checkpoint.pictureName) {
            case '5eb623c7-2b65-48f6-aaed-229628e7128f':
                // Rotonde
                badge = await Badge.findOne({ name: 'Un poil de culture' });
                quest.checkpoints[quest.checkpoints.indexOf(checkpoint)]._idBadge = badge._id;
                break;
            case '7c76e426-45eb-41a0-bcba-c90b76d01c8f':
                // Rhino
                badge = await Badge.findOne({ name: 'Urhino retrouvé' });
                quest.checkpoints[quest.checkpoints.indexOf(checkpoint)]._idBadge = badge._id;
                break;
            case '16d83266-5f2c-49fe-8d0b-5bfe0173196e':
                // TC
                badge = await Badge.findOne({ name: 'Apprenti TC' });
                quest.checkpoints[quest.checkpoints.indexOf(checkpoint)]._idBadge = badge._id;
                break;
            case 'b4fcf88d-40c6-4286-a41a-f01ae0281b10':
                // Mur INSA
                badge = await Badge.findOne({ name: 'Newbie de l\'INSA' });
                quest.checkpoints[quest.checkpoints.indexOf(checkpoint)]._idBadge = badge._id;
                break;
            case 'de08ab57-0daa-463e-a63d-9eadb4752c43':
                // K-Fêt
                badge = await Badge.findOne({ name: 'Fest\'IF' });
                quest.checkpoints[quest.checkpoints.indexOf(checkpoint)]._idBadge = badge._id;
                break;
        }
    }
    await quest.save();

    // eslint-disable-next-line
    console.log('Data associated.');

    process.exit(0);
});
