using Castle.DynamicProxy;
using ObserverPatternSample;
using System;

namespace AOP_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var proxyClient = GetInterfaceProxy<IMyClient>(new MyClient(), new DoSomethingAspect());
            proxyClient.GetOtherOne();
            proxyClient.GetOtherTwo();
        }

        static T GetInterfaceProxy<T>(T instance, params IInterceptor[] interceptors)
        {
            if (!typeof(T).IsInterface)
                throw new Exception("T should be an interface");

            ProxyGenerator proxyGenerator = new ProxyGenerator();
            return
                (T)proxyGenerator.CreateInterfaceProxyWithTarget(typeof(T), instance, interceptors);
        }
    }
}
