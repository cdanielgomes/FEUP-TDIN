const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');

const Question = require('../models/question.model.js');


router.get('/seed', async (req, res) => {
    await User.remove({})
    await Issue.remove({})
    await Question.remove({})
    res.send();
});

module.exports = router