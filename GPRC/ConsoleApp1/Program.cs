using Grpc.Net.Client;
using GrpcService2;
using GrpcService2.Protos;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);

            var sayHello = client.SayHello(new HelloRequest { Name = "123" });
            var sayHelloAsync = await client.SayHelloAsync(new HelloRequest { Name = "123" });

            var client1 = new ProFirst.ProFirstClient(channel);
            var client1SayHello = client1.SayHello(new LxpRequest { Name = "123", Age = "123" });
            var userInfo = client1.GetUserInfo(new LxpRequest { Name = "321", Age = "abc" });
            var userInfoAsync = await client1.GetUserInfoAsync(new LxpRequest { Name = "321", Age = "abc" });
            Console.WriteLine("Hello World!");
        }
    }
}
