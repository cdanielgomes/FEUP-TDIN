var express = require('express');
const jwt = require('jsonwebtoken');
var router = express.Router();
var User = require('../models/user.model.js');

const secret = process.env.CENTRAL_SERVER_SECRET;

router.post('/login', (req, res) => {
  if (req.body.email && req.body.password) {
    User.authenticate(req.body.email, req.body.password, (error, user) => {
      if (error || !user) {
        return res.status(400).json({
          message: error.message,
          error: error
        });
      } else {
        req.session.email = user.email;

        const payload = {
          email: user.email,
          username: user.username,
          role: user.role,
        };

        const token = jwt.sign(payload, secret, {
          expiresIn: '24h',
        });

        return res.status(200).json({
          email: user.email,
          username: user.username,
          role: user.role,
          auth_token: token,
        });
      }
    });
  } else {
    var err = new Error('All fields required.');
    return res.status(400).json({
      message: err.message,
      error: err
    });
  }
});

router.get('/', (req, res) => {
  if (req.session.email) {
    // delete session object
    req.session.destroy((err) => {
      if (err) {
        return res.json({
          message: err.message,
          error: err
        });
      } else {
        res.status(200);
        return res.json({ message: "Logout successful" });
      }
    });
  }
  else {
    const err = new Error("No User is logged");
    err.status = 404;
    res.status(err.status);
    return res.json({
      message: err.message,
      error: err
    });
  }
});

module.exports = router;