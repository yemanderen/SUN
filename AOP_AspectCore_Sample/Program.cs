using System;

namespace AOP_AspectCore_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ICustomService customer = new CustomService();
            customer.Call();
        }
    }
}
