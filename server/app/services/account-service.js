const Account = require('../models/account.js');

async function getAccountById(accountId) {
    return Account.findById(accountId);
}

async function createAccount(account) {
    return account.save();
}

async function updateAccount(account) {
    const condition = { _id: account._id };
    return Account.update(condition, account);
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
