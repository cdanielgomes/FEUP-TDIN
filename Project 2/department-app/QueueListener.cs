using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Department {
    class QueueListener {
        const String HOST_ADDRESS = "localhost";
        const int PORT = 5672;
        const String EXCHANGE_NAME = "issues";

        public delegate void MessageReceivedEvent(String msg);
        public MessageReceivedEvent Received = null;

        public QueueListener() {
            var factory = new ConnectionFactory() { HostName = HOST_ADDRESS, Port = PORT };
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);
                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName,
                              exchange: EXCHANGE_NAME,
                              routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) => {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        if (Received != null) Received(message);
                    };
                    channel.BasicConsume(queue: queueName,
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
        }
    }
}