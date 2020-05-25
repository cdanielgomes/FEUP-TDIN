"use strict";
const nodemailer = require("nodemailer");
const moment = require("moment")
const send = (issue, solver) => {
    
    if(!(issue && solver)) {
        console.log("COuldnt send email")
        return false
    } 
     
  let transporter = nodemailer.createTransport({
    service: "gmail",
     auth: {
      user: "tdin2020solver@gmail.com", // generated ethereal user
      pass: "solvertdin2020", // generated ethereal password
    },
  });

  // send mail with defined transport object
  console.log("try to send")
transporter.sendMail({
    from: `"${solver.email}" ${solver.email}`, // sender address
    to: `${issue.creator}`, // list of receivers
    subject: `Issue "${issue.title}" submited has been solved`, // Subject line
    html: `<h1> The issue you submitted "${issue.title}" has been solved!!! </h1> \
    <br/>\
    <h2> Issue: </h2>\
    <h4> Title: <h4> ${issue.title}\
    <h4> Description: <h4>${issue.description}\
    <h4> Submitted at: <h4>${moment(issue.createdAt).format('LLLL')}}\
    <h2> Was solved by: <h2> ${solver.name}\
    <h4> Answer: </h4>${issue.answer}`
  }).then(res => console.log("email sent: " + res.response))
  .catch(err => console.log(err))
}

module.exports = send