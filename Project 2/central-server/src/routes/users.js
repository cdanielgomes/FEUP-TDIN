const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');

router.post('/', (req, res) => {
  console.log(req.body);
  if (req.body.password !== req.body.passwordConf) {
    var err = new Error('Passwords do not match.');
    err.status = 401;
    res.status(err.status).json({
      message: err.message,
      error: err
    });
  }

  if (req.body.email &&
    req.body.username &&
    req.body.password &&
    req.body.passwordConf &&
    req.body.role) {

    var userData = {
      email: req.body.email,
      username: req.body.username,
      password: req.body.password,
      role: req.body.role,
    }

    User.create(userData, (error, user) => {
      
      if (error) {
        console.log(error)
        res.status(500).json({
          message: error.message,
          error: error
        });
      } else {
        req.session.userId = user._id;
        res.status(200).json({
          message: "User created with success"
        });
      }
    });

  } else {
    var err = new Error('All fields required.');
    err.status = 400;
    res.status(err.status).json({
      message: err.message,
      error: err
    });
  }
});

router.get('/reset', (req, res) => {
  User.remove({}, (err) => {
    if (err) {
      const err = new Error('Failed to reset Users.');
      err.status = 500;
      res.json({
        message: err.message,
        error: err
      });
    }
  });
});

router.get('/list', (req, res) => {
  User.find({}, (err, users) => {
    if (err) {
      const err = new Error('Failed to list Users.');
      err.status = 500;
      res.json({
        message: err.message,
        error: err
      });
    }
    res.status(200).json(users);
  });
});

module.exports = router;