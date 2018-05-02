const Account = require('../models/account.js');
const { createUserError } = require('../../helpers');

async function getAccountById(accountId) {
    let account = await Account.findById(accountId);
    if (!account) throw createUserError('Unknow account', 'No account found with the provided _id.');
    account = account.toJSON();
    // Remove sensitive fields
    delete account.connection.password;
    delete account.connection.jwt;
    return account;
}

async function getAccountByEmail(email) {
    // No need to remove sensitive fields (this method is used only on server side)
    return Account.findOne({ 'connection.email': email });
}

async function createAccount(account) {
    return account.save();
}

async function updateAccount(accountId, accountModif) {
    const actualAccount = await Account.findById(accountId);
    if (!actualAccount) throw createUserError('Unknow account', 'No account found with the provided _id.');
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
