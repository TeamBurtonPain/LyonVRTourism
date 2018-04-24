// TODO: const { createUserError } = require('../../utils');
const accountService = require('../services/account-service.js');

async function getAccountById(req, res) {
    const account = await accountService.getAccountById(req.params.id);

    res.json(account);
}

module.exports = {
    getAccountById,
};
