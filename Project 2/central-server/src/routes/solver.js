const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');
const Events = require('../middleware/events')
const Question = require('../models/question.model.js');
const { publishQueue } = require('../utils/lib/queue');
// get all issues of a solver

router.get("/", (req, res) => {
    Issue.find({ $or: [{ state: "unassigned" }, { assignee: req.userEmail }] }, (err, issues) => {
        if (err) return res.status(500).json({ message: err })
        else {
            res.status(200).json({ issues })
        }
    });
});


// update one issue
router.put("/:id/assigned", (req, res) => {
    setState("assigned", req, res)
});


// solve issue
router.put("/:id/solved", (req, res) => {

    setState("solved", req, res)
    // need to check if array of questions is empty
    // TODO: needs to send an email 
});

// get the questions of a issue
router.get("/:id/question", (req, res) => {
    Question.find({ issueId: req.params.id }, (err, questions) => {
        if (err) return res.status(500).json({ message: err });
        else {
            res.status(200).json({ questions });
        }
    });
});

// create question
router.post("/:id/question", (req, res) => {

    Question.create({ issueId: req.params.id, ...req.body }, (error, question) => {

        if (error) return res.status(500).json({ message: error.message })
        Issue.findByIdAndUpdate(req.params.id, { $push: { unsolved_questions: question._id }, state: "waiting" }, (err, issue) => {
            if (err) {
                Question.deleteOne({ _id: question._id });
                return res.status(500).json({ message: err.message });
            }

            res.status(200).json({
                question,
                message: "Question created with success"
            })

            Events.sendInfo("client", issue);
            publishQueue(JSON.stringify(question), question.department);
        })
    })
});

// get a question
router.get("/:id/questions/:questionId", (req, res) => {
    Question.findOne({ _id: req.params.questionId }, (err, question) => {
        if (err) return res.status(500).json({ message: err });
        else {
            res.status(200).json({ question });
        }
    });
});

// answer a question
router.put("/:id/questions/:questionId", (req, res) => {
    Question.findByIdAndUpdate(req.params.questionId, { answer: req.body.answer }, (err, question) => {
        if (err) return res.status(500).json({ message: err })
        else {
            Issue.findOne({_id: question.issueId}, {assignee}, (error, issue) => {
                if(error) return res.status(500).json({message:err})
    
                res.status(200).json({ question })
                Events.sendInfo("question", issue, question)
          
            })
        }
    });
});

router.get("/", (req, res) => {
    Issue.find({ $or: [{ state: "unassigned" }, { assignee: req.userEmail }] }, (err, issues) => {
        if (err) return res.status(500).json({ message: err })
        else {
            res.status(200).json({ issues })
        }
    })
});



const setState = (role, req, res) => {
    Issue.findByIdAndUpdate(req.params.id,
        { state: role, assignee: req.userEmail, resolution: req.body.answer },
        { new: true, timestamps: true },
        (err, issue) => {
            if (err) console.log(err)
            if (err) return res.status(500).json({ message: `Error updating Issue ${req.params.id}` })
            res.status(200).json({
                message: `Issue ${req.params.id} updated to \"${role}\"`,
                issue
            })

            Events.sendInfo("client", issue) // send event to client
        })
}



module.exports = router