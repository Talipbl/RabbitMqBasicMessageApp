

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
Console.WriteLine("Producer ile görüşme başlatıldı");
using (IConnection connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        channel.QueueDeclare("rabbitMqkuyruğu", true, false, true);
        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume("rabbitMqkuyruğu", false, consumer);
        consumer.Received += (sender, args) =>
        {
            var body = args.Body.Span;
            Console.WriteLine("{0,-20} {1,20}", " ", Encoding.UTF8.GetString(body));
        };

        while (true)
        {
            var message = Console.ReadLine();
            channel.QueueDeclare("rabbitMqkuyruğu2", true, false, true);
            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: "rabbitMqkuyruğu2", body: byteMessage);

        }
    }
}