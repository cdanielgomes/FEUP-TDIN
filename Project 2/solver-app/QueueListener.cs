using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Solver {
    class QueueListener {
        public delegate void MessageReceivedEvent(String msg);
        public MessageReceivedEvent Received;

        QueueListener() {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(queue: "trouble_tickets_queue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) => {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        if (Received != null) Received(message);
                    };
                    channel.BasicConsume(queue: "trouble_tickets_queue",
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
        }
    }
}