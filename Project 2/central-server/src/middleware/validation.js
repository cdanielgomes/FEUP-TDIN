const User = require('../models/user.model')

async function checkUserCanAcess(req, res, next) {
    if (!req.userEmail) return res.status(403).json({ message: "Nonexistent user" })
    const user = await User.findOne({ email: req.userEmail });

    if (user) next()
    else return res.status(403).json({ message: "Nonexistent user" })
    return
}

module.exports = checkUserCanAcess;