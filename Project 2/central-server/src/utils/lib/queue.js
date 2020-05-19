const amqp = require('amqplib/callback_api');
const logger = require('../logger');

const HOST_ADDRESS = process.env.HOST_ADDRESS;
const EXCHANGE_NAME = process.env.EXCHANGE_NAME;

const publishChannel = new Promise((resolve, reject) => {
  amqp.connect(HOST_ADDRESS, (connectionError, connection) => {
    if (connectionError) {
      console.log(connectionError);
      reject(connectionError);
    }
    connection.createChannel((channelError, channel) => {
      if (channelError) {
        console.log(channelError);
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
  const channel = await publishChannel;
  channel.publish(EXCHANGE_NAME, '', Buffer.from(msg));
  logger.info(`Published into queue: ${msg}`);
};

module.exports = {
  publishQueue,
};