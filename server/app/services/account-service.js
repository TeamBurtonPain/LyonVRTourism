async function getAccountById(accountId) {
    return {
        id: accountId,
        name: 'Noël Flantier',
    };
}

async function createAccount(account) {
    return account.save();
}

module.exports = {
    getAccountById,
    createAccount,
};
