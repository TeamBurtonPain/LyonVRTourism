const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const Quest = new Schema({
    _idCreator: Schema.Types.ObjectId,
    geo: {
        lat: Number,
        long: Number
    },
    title: {
        type: String,
        required: true
    },
    description: {
        type: String,
        required: true
    },
    picturePath: {
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
            enigmAnswer: String,
            difficulty: {
                type: Number,
                required: true,
                min: 0,
                max: 5
            } // O < difficulty < 5
        }
    ],
    feedbacks: [
        {
            _id: Schema.Types.ObjectId,
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
    disable: {
        type: Boolean,
        required: true
    }
});

module.exports = mongoose.model('Quest', Quest);
