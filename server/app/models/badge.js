const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const Badge = new Schema({
    name: String,
    description: String,
    picturePath: String,
    earn: Number,
});

module.exports = mongoose.model('Badge', Badge);
