const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');
const Events = require('../middleware/events')

// get all issues of a worker

router.get("/", (req, res) => {
    Issue.find({ creator: req.userEmail }, (err, issues) => {
        if (err) return res.status(500).json({ message: err })
        else {
            const message = "Get issues with success"
            res.status(200).json({ issues, message })
        }
    })
});


// add one issue
router.post("/", (req, res) => {
    Issue.create({ ...req.body, creator: req.userEmail }, (error, answer) => {
        if (error) {
            res.status(500).json({ message: error })
        } else {
            res.status(200).json({
                issue: answer,
                message: "Inserted with success"
            })

            ///Events.sendInfo("issue", answer)
        }
    })
});



router.get('/reset', (req, res) => {

    Issue.remove({}, (err) => {
        if (err) {
            const err = new Error('Failed to reset');
            err.status = 500;
            res.json({
                message: err.message,
                error: err
            });
        }

        res.status(200).json('All cleared');
    });
});


module.exports = router