const accountService = require('../services/account-service.js');
const Account = require('../models/account.js');

async function getAccountById(req, res) {
    const account = await accountService.getAccountById(req.params.id);

    res.json(account);
}

async function createAccount(req, res) {
    let newAccount = new Account(req.body);

    newAccount = await accountService.createAccount(newAccount);

    res.json(newAccount);
}

async function updateAccount(req, res) {
    let newAccount = new Account(req.body);

    newAccount = await accountService.updateAccount(newAccount);

    res.json(newAccount);
}

async function deleteAccount(req, res) {
    await accountService.deleteAccount(req.params.id);

    res.sendStatus(200);
}

module.exports = {
    getAccountById,
    createAccount,
    updateAccount,
    deleteAccount
};
