const accountService = require('../services/account-service');
const am = require('../../helpers/async-middleware');
const { createServerError } = require('../../helpers');

function roleGuard(role) {
    return am(async (req, res, next) => {
        const account = await accountService.getAccountById(req.user.accountId);
        switch (role) {
            case 'ME':
                if (account._id !== req.params['id'] || account.userInformation.accountType !== 'ADMIN') {
                    throw createServerError('Permission Denied', 'You don\'t have enough permission.');
                }
                break;
            case 'CREATOR':
                if (account.userInformation.accountType !== 'CREATOR' || account.userInformation.accountType !== 'ADMIN') {
                    throw createServerError('Permission Denied', 'You don\'t have enough permission.');
                }
                break;
            case 'ADMIN':
                if (account.userInformation.accountType !== 'ADMIN') {
                    throw createServerError('Permission Denied', 'You don\'t have enough permission.');
                }
                break;
        }
        next();
    });
}

module.exports = roleGuard;
