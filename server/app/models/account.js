// TODO: const { createUserError } = require('../../utils');
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const Account = new Schema({
    name: String,
});

module.exports = mongoose.model('Account', Account);

// TODO: complete schema
// {
//     id: 'idAccount',
//     connexion: {
//         email: 'noel.flantier@insa-lyon.fr', // Optional
//         password: 'encryptedPassword' // Optional
//     },
//     userInformation: {
//         lastName: 'Flantier', // Server side only and optional
//         firstName: 'Noël', // Server side only and optional
//         dateOfBirth: 'DATE', // Server side only and optional
//         username: 'nFlantier',
//         accountType: 'ADMIN, EDITOR, GAMER',
//         idEditor: '' // Null for a gamer
//     },
//     dates: {
//         createdAt: '',
//         updatedAt: ''
//     },
//     game: {
//         badges: [
//             'idBadge1',
//             'idBadge2'
//         ],
//         quests: [
//             {
//                 idQuest: 'idQuest',
//                 state: 'ENUM[IN_PROGRESS, DONE]',
//                 stats: {
//                     earnedXp: 10,
//                 },
//                 idFeedback: ''
//             }
//         ],
//         xp: 0,
//         elapsedTime: 123 //Sec
//     }
// }
