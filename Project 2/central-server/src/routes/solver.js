const express = require('express');
const router = express.Router();
const User = require('../models/user.model.js');
const Issue = require('../models/issue.model.js');

// get all issues of a solver

router.get("/", (req, res) => {
    Issue.find({ $or: [{ state: "unassigned" }, { assignee: req.userEmail }] }, (err, issues) => {
        if (err) return res.status(500).json({ message: err })
        else {
            res.status(200).json({ issues })
        }
    })
});


// update one issue
router.put("/assigned/:id", (req, res) => {
     setState("assigned", req, res)
});


router.put("/solved/:id", (req, res) => {
    setState("solved", req, res)
    // TODO: needs to send an email 
});



const setState = (role, req, res) => {
    Issue.findByIdAndUpdate(req.params.id,
        { state: role, assignee: req.userEmail },
        { new: true, timestamps: true },
        (err, issue) => {
          if(err) console.log(err)
            if (err) return res.status(500).json({ message: `Error updating Issue ${req.params.id}` })
            res.status(200).json({
                message: `Issue ${req.params.id} updated to \"${role}\"`,
                issue
            })
        })
}

module.exports = router