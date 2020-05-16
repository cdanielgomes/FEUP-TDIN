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
        enum: ["unassigned", "solved", "assigned"],
        default: "unassigned"
    },
    redirects: {
        type: Boolean,
        default: false,
    }
},
    {
        timestamps: true
    }
);

module.exports = mongoose.model('Issue', IssueSchema);