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
    const question = {
        "state": "queued",
        "_id": "5ecaa132707c180059f91720",
        "issueId": "5ecaa0fd707c180059f9171f",
        "question": "coiso?",
        "department": "Issues",
        "createdAt": "2020-05-24T16:30:42.196Z",
        "updatedAt": "2020-05-24T16:30:42.196Z",
        "__v": 0
      };      
    publishQueue(JSON.stringify(question), question.department);
    res.json('done');
});

module.exports = router