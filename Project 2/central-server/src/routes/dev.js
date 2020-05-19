const express = require('express');
const router = express.Router();
const { seedDb } = require('../utils/db/seed');
const { publishQueue } = require('../utils/lib/queue');

const Question = require('../models/question.model.js');

router.get('/seed', async (req, res) => {
    seedDb();
    res.json('done');
});

router.get('/queue', async (req, res) => {
    publishQueue("test");
    res.json('done');
});

module.exports = router