const Account = require('../models/account.js');

async function getAccountById(accountId) {
    return Account.findById(accountId);
}

async function createAccount(account) {
    return account.save();
}

async function updateAccount(accountId, accountModif) {
    const actualAccount = await Account.findById(accountId);
    actualAccount.merge(accountModif);
    return actualAccount.save();
}

async function deleteAccount(accountId) {
    const condition = { _id: accountId };
    return Account.remove(condition);
}

module.exports = {
    getAccountById,
    createAccount,
    updateAccount,
    deleteAccount,
};
