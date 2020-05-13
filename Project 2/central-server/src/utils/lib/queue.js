const amqp = require('amqplib/callback_api');
const logger = require('../logger');

let publishChannel;

const queue = 'trouble_tickets_queue';

amqp.connect('amqp://localhost', (connectionError, connection) => {
  if (connectionError) {
    throw connectionError;
  }
  connection.createChannel((channelError, channel) => {
    if (channelError) {
      throw channelError;
    }

    channel.assertQueue(queue, {
      durable: false
    });

    publishChannel = channel;
    channel.sendToQueue(queue, Buffer.from(msg));
  });
});

const publishQueue = (msg) => {
  publishChannel.sendToQueue(msg);
  logger.info(`Published into queue: ${msg}`);
};

module.exports = {
  publishQueue,
};