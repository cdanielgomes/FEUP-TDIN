const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');
const Events = require('../middleware/events')
const Question = require('../models/question.model.js');
const { publishQueue } = require('../utils/lib/queue');
const send = require("../email/email")
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
router.put("/:id/solved", async (req, res) => {

    const issue = await Issue.findOne({ _id: req.params.id })
    console.log("ARE YOU HERE MOTHERDUCJKERA")
    const array = issue.unsolved_questions;
    console.log(array)

    if (array.length) {
        
        res.status(418).json({ message: "You have to wait for all question been solved. Be patient, take a coffee" })
        return
    }

    const issueResolved = setState("solved", req, res);
    console.log(issueResolved)
    const solver = await User.findOne({ _id: issueResolved.assignee })

    console.log("vou mandar alguma cena\n", solver, issueResolved)
    send(issueResolved, solver)

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

    // TODO notifications about the receiveing of an answer
    if (!req.body.answer) return res.status(422).json({ message: "Missing answer" })

    Question.findByIdAndUpdate(req.params.questionId, { answer: req.body.answer, state: "answered" }, { new: true }, (error, question) => {
        if (error) return res.status(500).json({ message: "Impossible update Question for issue" })
        Issue.updateOne({ _id: req.params.id }, { $pull: { unsolved_questions: req.params.questionId } }, (err, issue) => {
            if (err) {
                Question.updateOne({ _id: re.params.questionId }, { answer: null, state: "queued" })
                return res.status(500).json({ message: "Impossible update Question for issue" })
            }
            res.status(200).json({
                question,
                message: "Question updated with success"
            })

            Events.sendInfo("question", req.params.id, question)
        })
    })
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
            return issue
        })
}



module.exports = router