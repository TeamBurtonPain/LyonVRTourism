// TODO: const { createUserError } = require('../../utils');
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const Account = new Schema({
    connexion: {
        email: String, // Optional
        password: String // Optional
    },
    userInformation: {
        lastName: String, // Server side only and optional
        firstName: String, // Server side only and optional
        dateOfBirth: Date, // Server side only and optional
        username: String, // Pseudo
        accountType: {
            type: String,
            enum: ['ADMIN', 'EDITOR', 'GAMER'],
            default: 'GAMER'
        },
        idEditor: String // Null for a gamer
    },
    dates: {
        createdAt: Date,
        updatedAt: Date
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

module.exports = mongoose.model('Account', Account);
