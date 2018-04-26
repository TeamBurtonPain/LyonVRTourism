// TODO: const { createUserError } = require('../../utils');
const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const merge = require('mongoose-merge-plugin');
const mongooseBCrypt = require('mongoose-bcrypt');

const Account = new Schema({
    connection: {
        email: {
            type: String,
            validate: {
                validator: (v) => {
                    // eslint-disable-next-line
                    return /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/.test(v);
                }
            },
            required: true
        },
        password: {
            type: String,
            required: true,
            bcrypt: true
        },
        jwt: String
    },
    userInformation: {
        lastName: String, // Optional
        firstName: String, // Optional
        dateOfBirth: Date, // Optional
        username: {
            type: String,
            required: true
        }, // Pseudo
        accountType: {
            type: String,
            enum: {
                values: ['ADMIN', 'EDITOR', 'GAMER'],
                message: '`accountType` field must be one of these value [\'ADMIN\', \'EDITOR\', \'GAMER\']'
            },
            default: 'GAMER'
        },
        idEditor: String // Null for a gamer
    },
    dates: {
        createdAt: Date, // Auto
        updatedAt: Date // Auto
    },
    game: {
        badges: [Schema.Types.ObjectId], // Badges ObjectId Array
        quests: [
            {
                _idQuest: Schema.Types.ObjectId, // Quest ObjectId
                state: {
                    type: String,
                    enum: ['IN_PROGRESS', 'DONE'],
                    default: 'IN_PROGRESS'
                },
                stats: {
                    earnedXp: Number,
                },
                feedback: {
                    comment: String,
                    mark: Number
                } // May be duplicate this object to quest object
            }
        ],
        xp: Number,
        elapsedTime: Number // Sec
    }
});

Account.plugin(merge);
Account.plugin(mongooseBCrypt);

module.exports = mongoose.model('Account', Account);
