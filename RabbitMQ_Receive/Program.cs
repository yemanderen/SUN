using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace rabbirmq_receive
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("====Work模式开启了====");
            //ConsumeWorkQueueModel("workqueue", handserMsg: msg =>
            //{
            //    Console.WriteLine($"work模式获取到消息{msg}");
            //});
            //Console.ReadLine();


            #region 发布订阅模式 Exchange
            SubscriberExchangeModel("exchangemodel", msg =>
            {
                Console.WriteLine($"订阅到消息：{msg}");
            });
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

            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var message = Encoding.UTF8.GetString(body.ToArray());
            //        Console.WriteLine(" [x] Received {0}", message);
            //    };
            //    channel.BasicConsume(queue: "hello",
            //        autoAck: true,
            //        consumer: consumer);

            //    Console.WriteLine(" Press [enter] to exit.");
            //    Console.ReadLine();
            //}
        }

        //work后端逻辑
        public static void ConsumeWorkQueueModel(string queueName, int sleepHmao = 90, Action<string> handserMsg = null)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            Console.WriteLine(" ConsumeWorkQueueModel Waiting for messages....");

            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (handserMsg != null)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        handserMsg.Invoke(message);
                    }
                }
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }
        //订阅者后端的逻辑
        public static void SubscriberExchangeModel(string exchangeName, Action<string> handlerMsg = null)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);//Fanout 扇形分叉

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                     exchange: exchangeName,
                     routingKey: "");

            Console.WriteLine(" Waiting for msg....");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (handlerMsg != null)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        handlerMsg.Invoke(message);
                    }
                }
                else
                {
                    Console.WriteLine($"订阅到消息:{message}");
                }
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
