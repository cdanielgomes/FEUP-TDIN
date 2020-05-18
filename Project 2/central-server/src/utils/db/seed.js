const User = require("../../models/user.model");
const Issue = require("../../models/issue.model");
const Question = require("../../models/question.model");

User.collection.drop();
Issue.collection.drop();
Question.collection.drop();

const seedDb = () => {
    User.create([
        {
            email: "daniel@mail.com",
            username: "Daniel Gomes",
            password: "123456",
            role: "worker"
        },
        {
            email: "joao@mail.com",
            username: "Joao Maduro",
            password: "123456",
            role: "solver"
        },
    ]);

    Issue.create([
        {
            title: "Ligma",
            description: "Apanha ligma",
            creator: "daniel@mail.com"
        },
        {
            title: "Sol",
            description: "Apanha sol",
            creator: "joao@mail.com"
        },
    ]);
};

module.exports = {
    seedDb,
}