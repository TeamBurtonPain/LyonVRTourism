const { createUserError } = require('../../helpers');
const accountService = require('../services/account-service');
const jwtConfig = require('../../config/jwt');
const jwt = require('jsonwebtoken');
const uuidv4 = require('uuid-v4');

async function login(req, res) {
    if ((!req.body.email || !req.body.password) && req.body.email.length > 0 && req.body.password.length > 0) {
        throw createUserError('BadRequest', 'Your request body must match { email: String, password: String }');
    }

    const account = await accountService.getAccountByEmail(req.body.email);

    if (!account) {
        throw createUserError('LoginError', 'Wrong email/password combinaison');
    }

    // Use mongoose-bcrypt plugin function :)
    const passwordVerif = await account.verifyConnectionPassword(req.body.password);

    if (!passwordVerif) {
        throw createUserError('LoginError', 'Wrong email/password combinaison');
    }

    const jwtId = uuidv4();

    const jwtPayload = {
        accountId: account._id,
        id: jwtId
    };
    const token = jwt.sign(jwtPayload, jwtConfig.secret);

    await accountService.updateAccount(account._id, { connection: { jwt: jwtId } });
    res.json({ jwt: token });
}

async function logout(req, res) {
    const currentAccount = await accountService.getAccountById(req.user.accountId);

    currentAccount.connection.jwt = null;

    const updatedAccount = await accountService.updateAccount(req.user.accountId, currentAccount);

    res.json(updatedAccount);
}

module.exports = {
    login,
    logout,
};
