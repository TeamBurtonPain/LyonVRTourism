const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const Quest = new Schema({
    _idCreator: Schema.Types.ObjectId,
    geo: {
        lat: Number,
        long: Number
    },
    description: String,
    picturePath: String,
    checkpoints: [
        {
            photoPath: String,
            text: String,
            enigmAnswer: String,
            difficulty: Number // O < difficulty < 5
        }
    ],
    feedbacks: [
        {
            _id: Schema.Types.ObjectId,
            _idAccount: Schema.Types.ObjectId,
            comment: String,
            mark: Number // 0 < note < 10
        }
    ],
    disable: Boolean
});

module.exports = mongoose.model('Quest', Quest);
