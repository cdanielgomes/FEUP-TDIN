const mongoose = require('mongoose');

const QuestionSchema = new mongoose.Schema({

    issueId: { type: mongoose.Types.ObjectId },
    question: { type: String, required: true },
    answer: { type: String },
    department: { type: String, required: true },
    state: { type: String, enum: ["answered", "queued"], default: "queued" },
},
    { timestamps: true }
)


module.exports = mongoose.model("Question", QuestionSchema)