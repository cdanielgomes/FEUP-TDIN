const jwt = require('jsonwebtoken');
const logger = require('../utils/logger')

function checkJWTandRole(req, res, next) {
    try {
        roles = this;

        const token = req.headers["auth_token"];
        if (!token) return res.status(401).json({ message: "No token provided" })

        const secret = process.env.CENTRAL_SERVER_SECRET;

        jwt.verify(token, secret, function (err, decoded) {
            if (err) return res.status(500).send({ auth: false, message: 'Failed to authenticate token.' });

            req.userEmail = decoded.email
            req.userName = decoded.name
            req.userRole = decoded.role
            if (!roles.includes(decoded.role)) return res.status(401).json({ message: "Unauthorized" })


            next()
        });

    } catch (e) {
        logger.error(e);
    }
}

module.exports = checkJWTandRole