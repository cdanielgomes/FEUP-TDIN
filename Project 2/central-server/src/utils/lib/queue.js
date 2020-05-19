const amqp = require('amqplib/callback_api');
const logger = require('../logger');

const HOST_ADDRESS = process.env.HOST_ADDRESS;
const QUEUE_NAME = process.env.QUEUE_NAME;

let publishChannel;

const createChannel = () => new Promise((resolve, reject) => {
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

      channel.assertQueue(QUEUE_NAME, {
        durable: true
      });

      publishChannel = channel;

      resolve(publishChannel);

    });
  });
});

console.log(process.env.QUEUE_NAME);

const getChannel = async () => {
  if (publishChannel) return publishChannel;
  return await createChannel();
}

const publishQueue = async (msg) => {
  const channel = await getChannel();
  channel.sendToQueue(QUEUE_NAME, Buffer.from(msg), {
    persistent: true
  });
  
  logger.info(`Published into queue: ${msg}`);
};

module.exports = {
  publishQueue,
};