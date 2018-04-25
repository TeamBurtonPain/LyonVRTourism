const { createUserError } = require('../../helpers');

async function login(req, res) {
    if ((!req.body.email || !req.body.password) && req.body.email.length > 0 && req.body.password.length > 0) {
        throw createUserError('BadRequest', 'Your request body must match { email: String, password: String }');
    }

    res.json({});
}

async function logout(req, res) {
    res.json({});
}

module.exports = {
    login,
    logout,
};
