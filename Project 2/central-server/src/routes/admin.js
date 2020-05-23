const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');

const Question = require('../models/question.model.js');


router.get('/', async (req, res) => {
    await User.remove({})
    await Issue.remove({})
    await Question.remove({})
    res.send();
})


router.get('/users', async (req, res) => {
    const users = await User.find({})
    res.status(200).json(users)
})

router.get('/questions', async (req, res) => {
    const questions = await Question.find({})
    res.status(200).json(questions)
})

router.get('/issues', async (req, res) => {
    const issues = await Issue.find({})
    res.status(200).json(issues)
})

module.exports = router