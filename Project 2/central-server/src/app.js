const createError = require('http-errors');
const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');
const cookieParser = require('cookie-parser');
const mongoose = require('mongoose');
const httplogger = require('morgan');
const logger = require('./utils/logger');

const indexRouter = require('./routes/index');
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

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

app.use(httplogger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', indexRouter);
app.use('/users', usersRouter);

// catch 404 and forward to error handler
app.use(function (req, res, next) {
  next(createError(404));
});

// error handler
app.use(function (err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;
