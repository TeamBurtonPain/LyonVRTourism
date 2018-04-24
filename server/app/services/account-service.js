async function getAccountById(accountId) {
    return {
        id: accountId,
        name: 'Noël Flantier',
    };
}

module.exports = {
    getAccountById,
};
