const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const mongooseMerge = require('mongoose-merge-plugin');
const mongooseBCrypt = require('mongoose-bcrypt');
const mongooseTimestamp = require('mongoose-timestamp');

const Quest = new Schema({
    _idCreator: Schema.Types.ObjectId,
    geolocalisation: {
        x: Number,
        y: Number
    },
    title: {
        type: String,
        required: true
    },
    description: {
        type: String,
        required: true
    },
    checkpoints: [
        {
            pictureName: {
                type: String,
                required: true
            },
            picturePath: {
                type: String,
                required: true
            },
            text: {
                type: String,
                require: true
            },
            question: {
                type: String,
                require: true
            },
            choices: [String],
            enigmAnswer: {
                type: String,
                require: true
            },
            difficulty: {
                type: Number,
                required: true,
                min: 0,
                max: 5
            }, // O < difficulty < 5
            _idBadge: Schema.Types.ObjectId
        }
    ],
    dates: {
        createdAt: Date, // Auto
        updatedAt: Date // Auto

    },
    statistics: [
        {
            _idAccount: Schema.Types.ObjectId,
            comment: {
                type: String,
                required: true
            },
            mark: {
                type: Number,
                required: true,
                min: 0,
                max: 10
            } // 0 < note < 10
        }
    ],
    open: {
        type: Boolean,
        required: true
    }
});

Quest.plugin(mongooseMerge);
Quest.plugin(mongooseBCrypt);
Quest.plugin(mongooseTimestamp);

module.exports = mongoose.model('Quest', Quest);
