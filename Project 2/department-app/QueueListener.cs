using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Department {
    class QueueListener {
        const String HOST_ADDRESS = "localhost";
        const int PORT = 5672;
        const String QUEUE_NAME = "issues";

        public delegate void MessageReceivedEvent(String msg);
        public MessageReceivedEvent MessageReceived = null;

        public QueueListener() {
            var factory = new ConnectionFactory() { HostName = HOST_ADDRESS, Port = PORT };
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(queue: QUEUE_NAME,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) => {
                        Console.WriteLine("Chegou mensagem");
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
    }
}