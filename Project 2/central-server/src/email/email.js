"use strict";
const nodemailer = require("nodemailer");
const moment = require("moment")
const send = (issue, solver) => {
 
    if (!(issue && solver)) {
        return false
    }

    let transporter = nodemailer.createTransport({
        service: "gmail",
        auth: {
            user: "tdin2020solver@gmail.com", // generated ethereal user
            pass: "solvertdin2020", // generated ethereal password
        },
    });

    const options  = {
        from: `"${solver.username}" ${solver.email}`, // sender address
        to: `${issue.creator}`, // list of receivers
        subject: `Issue "${issue.title}" submited has been solved`, // Subject line
        html: `<h1> The issue you submitted "${issue.title}" has been solved!!! </h1> \
    <br/>\
    <h2> Issue: </h2>\
    <h4> Title: ${issue.title} <h4>\
    <h4> Description:${issue.description}  <h4>\
    <h4> Submitted at: ${moment(issue.createdAt).format('LLLL')} <h4>\
    <h2> Was solved by: ${solver.username} <h2> \
    <h4> Answer: ${issue.resolution} </h4>`
    }

    transporter.sendMail(options).then(res => console.log("email sent: " + res.response))
    .catch(err => console.log(err))
}

module.exports = send