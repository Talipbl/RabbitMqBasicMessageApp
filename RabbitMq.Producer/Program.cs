

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
using (IConnection connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        channel.QueueDeclare("rabbitMqkuyruğu", true, false, true);
        byte[] byteMessage = Encoding.UTF8.GetBytes("Producer ile görüşme başlatıldı.");
        channel.BasicPublish(exchange: "", routingKey: "rabbitMqkuyruğu", body: byteMessage);

        channel.QueueDeclare("rabbitMqkuyruğu2", true, false, true);
        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume("rabbitMqkuyruğu2", false, consumer);
        consumer.Received += (sender, args) =>
        {
            var body = args.Body.Span;
            Console.WriteLine(Encoding.UTF8.GetString(body));
        };
        while (true)
        {
            var message = Console.ReadLine();

            byteMessage = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: "rabbitMqkuyruğu", body: byteMessage);

        }
    }
}