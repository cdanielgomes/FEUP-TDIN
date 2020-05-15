const amqp = require('amqplib/callback_api');
const logger = require('../logger');

const HOST_ADDERSS = 'amqp://localhost';
const EXCHANGE_NAME = 'issues';

const publichChannel = new Promise((resolve, reject) => {
  amqp.connect(HOST_ADDERSS, (connectionError, connection) => {
    if (connectionError) {
      reject(connectionError);
    }
    connection.createChannel((channelError, channel) => {
      if (channelError) {
        reject(channelError);
      }

      channel.assertExchange(EXCHANGE_NAME, 'fanout', {
        durable: false
      });

      resolve(channel);
      
    });
  });
});

const publishQueue = async (msg) => {
  const channel = await publichChannel;
  channel.publish(EXCHANGE_NAME, '', Buffer.from(msg));
  logger.info(`Published into queue: ${msg}`);
};

module.exports = {
  publishQueue,
};