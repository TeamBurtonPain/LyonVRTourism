// TODO: const { createUserError } = require('../../utils');
const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const merge = require('mongoose-merge-plugin');

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
            required: true
        }
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
                values: ['ADMIN', 'CREATOR', 'USER'],
                message: '`accountType` field must be one of these value [\'ADMIN\', \'CREATOR\', \'USER\']'
            },
            default: 'USER'
        }
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
                }
            }
        ],
        xp: Number,
        elapsedTime: Number // Sec
    }
});

Account.plugin(merge);

module.exports = mongoose.model('Account', Account);
