const mongoose = require('mongoose');
const Schema = mongoose.Schema;

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
    picture: {
        type: String,
        required: true
    },
    checkpoints: [
        {
            photoPath: {
                type: String,
                required: true
            },
            text: String,
            choices: {
                type: [String],
                required: true
            },
            answer: String,
            difficulty: {
                type: Number,
                required: true,
                min: 0,
                max: 5
            } // O < difficulty < 5
        }
    ],
    statistics: [
        {
            _idUser: Schema.Types.ObjectId,
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

module.exports = mongoose.model('Quest', Quest);
