export const quest = {
    id: 'idQuest',
    creator: 'idAccount',
    geo: {
        lat: '',
        long: ''
    },
    description: '',
    picturePath: '',
    checkpoints: [
        {
            id: 'checkpointId',
            photoPath: '',
            text: '',
            enigmAnswer: '',
            difficulty: 0 // O < difficulty < 5
        }
    ],
    feedbacks: [
        {
            id: 'idFeedback',
            idAccount: 'idAccount',
            note: 0 // 0 < note < 10
        }
    ],
    disable: true //
}
