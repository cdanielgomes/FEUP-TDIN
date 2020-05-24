const amqp = require('amqplib/callback_api');
const logger = require('../logger');

const HOST_ADDRESS = process.env.HOST_ADDRESS;

const queuesMap = {};

const createChannel = (queue) => new Promise((resolve, reject) => {
  amqp.connect(HOST_ADDRESS, (connectionError, connection) => {
    if (connectionError) {
      logger.error(connectionError);
      reject(connectionError);
    }
    connection.createChannel((channelError, channel) => {
      if (channelError) {
        logger.error(channelError);
        reject(channelError);
      }

      channel.assertQueue(queue, {
        durable: true
      });

      resolve(channel);

    });
  });
});

const getChannel = async (queue) => {
  if (queuesMap[queue]) return queuesMap[queue];
  const newQueue = await createChannel(queue);
  queuesMap[queue] = newQueue;
  return newQueue;
}

const publishQueue = async (msg, queue) => {
  const channel = await getChannel(queue);
  channel.sendToQueue(queue, Buffer.from(msg), {
    persistent: true
  });
  
  logger.info(`Published into queue: ${msg}`);
};

module.exports = {
  publishQueue,
};