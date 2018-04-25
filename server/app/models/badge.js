const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const Badge = new Schema({
    name: String,
    description: String,
    iconPath: String,
    require: {
        xp: Number,
        totalElapsedTime: Number,
        // TODO: quests: [Schema.Types.ObjectId],
        // checkpoints: [Schema.Types.ObjectId],
    },
    earn: Number,
});

module.exports = mongoose.model('Badge', Badge);
