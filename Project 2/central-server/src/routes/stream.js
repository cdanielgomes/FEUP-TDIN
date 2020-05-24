const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');
const Events = require('../middleware/events')
const Question = require('../models/question.model.js');


router.get('/worker', (req, res, next) => {
    Events.addClient(req, res);
    //next()
})

router.get('/solver', (req, res, next) => {
    Events.addClient(req, res);
    //next()
})
module.exports = router