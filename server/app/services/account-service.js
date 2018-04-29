const Account = require('../models/account.js');

async function getAccountById(accountId) {
    return Account.findById(accountId);
}

async function getAccountByEmail(email) {
    return Account.findOne({ 'connection.email': email });
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

async function isTokenRevoked(jwtId) {
    const account = await Account.findOne({ 'connection.jwt': jwtId });

    return !account || account.connection.jwt;
}

module.exports = {
    getAccountById,
    getAccountByEmail,
    createAccount,
    updateAccount,
    deleteAccount,
    isTokenRevoked,
};
