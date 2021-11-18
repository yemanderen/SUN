using System;
using System.Text;
using RabbitMQ.Client;

namespace rabbitmq_send
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 500; i++)
            //{
            //    PublishWorkQueueModel("workqueue", $" :发布消息成功{i}");
            //}
            //Console.WriteLine("工作队列模式 生成完毕......！");
            //Console.ReadLine();

            #region 发布订阅模式，带上了exchange
            for (int i = 0; i < 500; i++)
            {
                PublishExchangeModel("exchangemodel", $"发布的消息是：{i}");
            }
            Console.WriteLine("发布ok！");
            #endregion
            Console.ReadLine();


            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "hello",
            //        durable: false,
            //        exclusive: false,
            //        autoDelete: false,
            //        arguments: null);
            //    while (true)
            //    {
            //        var input = Console.ReadLine();
            //        string message = input;
            //        var body = Encoding.UTF8.GetBytes(message);

            //        channel.BasicPublish(exchange: "",
            //         routingKey: "hello",
            //         basicProperties: null,
            //         body: body);
            //        Console.WriteLine(" [x] Sent {0}", message);
            //    }

            //}

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }

        //生产者后端逻辑
        public static void PublishWorkQueueModel(string queueName, string msg)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(msg);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);
                Console.WriteLine($"{DateTime.Now},SentMsg: {msg}");
            }
        }

        //发布者的后端逻辑 我在这里选择了扇形: ExchangeType.Fanout
        public static void PublishExchangeModel(string exchangeName, string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);
                Console.WriteLine($" Sent {message}");
            }
        }
    }
}
