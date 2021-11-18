using System;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {

        static void Main(string[] args)
        {
            test3();
            Console.WriteLine(".......按任意键退出");
            Console.ReadKey();
        }

        static async void test()
        {
            while (true)
            {
                await Task.Delay(1000);
                Console.WriteLine(DateTime.Now.ToString("MM:ss.fff"));
            }

        }

        static async void test2()
        {
            while (true)
            {
                Task.Delay(1000);
                Console.WriteLine(DateTime.Now.ToString("MM:ss.fff"));
            }

        }

        static async Task<int> test3()
        {
            Console.WriteLine(DateTime.Now.ToString("MM:ss.fff"));
            await Task.Delay(10000);
            Console.WriteLine(DateTime.Now.ToString("MM:ss.fff"));
            return 1;

        }

        static async Task<int> test4()
        {
            Console.WriteLine(DateTime.Now.ToString("MM:ss.fff"));
            Task.Delay(10000);
            Console.WriteLine(DateTime.Now.ToString("MM:ss.fff"));
            return 1;

        }
    }
}