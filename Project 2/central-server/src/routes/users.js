var express = require('express');
var router = express.Router();
var User = require('../models/user.model.js');

router.post('/', (req, res) => {
  if (req.body.password !== req.body.passwordConf) {
    var err = new Error('Passwords do not match.');
    err.status = 400;
    res.send("passwords dont match");
    res.status(err.status || 500);
    res.json({
      message: err.message,
      error: err
    });
  }

  if (req.body.email &&
    req.body.username &&
    req.body.password &&
    req.body.passwordConf &&
    req.body.role ) {

    var userData = {
      email: req.body.email,
      username: req.body.username,
      password: req.body.password,
      role: req.body.role,
    }

    User.create(userData, (error, user) => {
      if (error) {
        res.status(error.status || 500);
        res.json({
          message: error.message,
          error: error
        });
      } else {
        req.session.userId = user._id;
        res.status(200);
        res.json({
          message: "User created with success"
        });
      }
    });

  } else {
    var err = new Error('All fields required.');
    err.status = 400;
    res.status(err.status || 500);
    res.json({
      message: err.message,
      error: err
    });
  }
});

module.exports = router;