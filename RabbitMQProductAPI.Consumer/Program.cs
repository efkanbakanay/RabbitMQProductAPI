﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.HostName = "localhost";

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("product", exclusive: false);


var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Ürün mesajı alındı: {message}");
};

channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);

Console.ReadKey();