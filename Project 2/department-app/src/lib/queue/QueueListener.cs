using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Department {
    class QueueListener {
        const String HOST_ADDRESS = "localhost";
        const int PORT = 5672;
        const String QUEUE_NAME = "Issues";

        public delegate void MessageReceivedEvent(String msg);
        public MessageReceivedEvent MessageReceived = null;

        public IConnection connection;
        public EventingBasicConsumer consumer;
        public IModel channel;

        public QueueListener() {
            var factory = new ConnectionFactory() { HostName = HOST_ADDRESS, Port = PORT };
            factory.UserName = "guest";
            factory.Password = "guest";
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: QUEUE_NAME,
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            consumer = new EventingBasicConsumer(channel);
        }

        public void Init() {
            consumer.Received += (model, ea) => {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                if (MessageReceived != null) MessageReceived(message);
            };
            channel.BasicConsume(queue: QUEUE_NAME,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}