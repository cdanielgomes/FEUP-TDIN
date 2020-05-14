var express = require('express');
var router = express.Router();
var User = require('../models/user.model.js');
var Issue = require('../models/issue.model.js');

router.post('/', (req, res) => {
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
        res.status(error.status).json({
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



// get all issues
router.get("/issue", (req, res) => {
  console.log("get all issues")
  res.status(200).json({
    issues: []
  })
})

// add one issue
router.post("/issue", (req, res) => {
  console.log("add one issue")
  res.status(200).json({
    issues: []

  })
})


// update one issue
router.put("/issue", (req, res) => {

  console.log("update issue")
  res.status(200).json({
    issues: []

  })
})

module.exports = router;