const User = require("../../models/user.model");
const Issue = require("../../models/issue.model");
const Question = require("../../models/question.model");

const seedDb = () => {
    User.collection.drop().catch((e) => { });
    Issue.collection.drop().catch((e) => { });
    Question.collection.drop().catch((e) => { });

    User.create([
        {
            email: "danielcfgomes@gmail.com",
            username: "Daniel Gomes",
            password: "123456",
            role: "worker"
        },
        {
            email: "carlosdcfgomes@hotmail.com",
            username: "Carlos Gomes",
            password: "123456",
            role: "worker"
        },
        {
            email: "joao@mail.com",
            username: "Joao Maduro",
            password: "123456",
            role: "solver"
        },
        {
            email: "dep@mail.com",
            username: "Department Technician",
            password: "123456",
            role: "solver"
        },
    ]);

    Issue.create([
        {
            title: "MIEIC at FEUP",
            description: "What do I need to make MIEIC course at Feup?",
            creator: "danielcfgomes@gmail.com"
        },
        {
            title: "TDIN",
            description: "Should I select TDIN in next year?",
            creator: "danielcfgomes@gmail.com"
        },
        {
            title: "Classes",
            description: "All classes at FEUP are at 8 a.m.?",
            creator: "danielcfgomes@gmail.com"
        },
        {
            title: "AIAD",
            description: "Professor Carlos Soares will be nice to me wednesday?",
            creator: "carlosdcfgomes@hotmail.com"
        },
    ]);
};

module.exports = {
    seedDb,
}