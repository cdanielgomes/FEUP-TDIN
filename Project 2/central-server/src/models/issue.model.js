const mongoose = require('mongoose');

const IssueSchema = new mongoose.Schema({
    creator: { type: String, required: true },
    assignee: { type: String, default: null},
    title: {
        type: String,
        required: true,
    },
    description: {
        type: String,
        required: true,
    },
    state: {
        type: String,
        enum: ["unassigned", "solved", "assigned", "waiting"],
        default: "unassigned"
    },
    resolution: {
        type : String,
    },
    unsolved_questions: [{type: mongoose.Types.ObjectId}]
},
    {
        timestamps: true
    }
);

module.exports = mongoose.model('Issue', IssueSchema);