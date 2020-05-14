const createError = require('http-errors');
const express = require('express');
const session = require("express-session");
const path = require('path');
const cors = require('cors');
const bodyParser = require('body-parser');
const cookieParser = require('cookie-parser');
const mongoose = require('mongoose');
const httplogger = require('morgan');
require("dotenv").config();

const logger = require('./utils/logger');

const indexRouter = require('./routes');
const authRouter = require('./routes/auth');
const usersRouter = require('./routes/users');

const app = express();

// Connection to DB
mongoose.connect('mongodb://mongo', {
  useUnifiedTopology: true,
  useNewUrlParser: true,
}).then(() => logger.info('Connection to Database succeeded'))
  .catch(err => {
    logger.warn(`Failed to connect to Database: ${ err.message }`);
  });

app.use(httplogger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));
app.use(cors())
//use sessions for tracking logins
app.use(session({
  secret: process.env.CENTRAL_SERVER_SECRET,
  resave: true,
  saveUninitialized: false
}));

app.use('/', indexRouter);
app.use('/api/auth', authRouter);
app.use('/api/users', usersRouter);

// catch 404 and forward to error handler
app.use((req, res, next) => {
  next(createError(404));
});

// error handler
app.use((err, req, res, next) => {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.json('error');
});

module.exports = app;
