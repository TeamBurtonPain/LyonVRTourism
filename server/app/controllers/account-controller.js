const accountService = require('../services/account-service.js');
const Account = require('../models/account.js');

async function getAccountById(req, res) {
    const account = await accountService.getAccountById(req.params.id);

    res.json(account);
}

async function createAccount(req, res) {
    let newAccount = new Account();

    newAccount.name = req.body.name;

    newAccount = await accountService.createAccount(newAccount);

    res.json(newAccount);
}

module.exports = {
    getAccountById,
    createAccount
};
