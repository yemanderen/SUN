using System;

namespace ObserverPatternSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var userService = new UserService();
            userService.LoginSucessEvent += new SmsObserver().Process;
            userService.LoginSucessEvent += new JifenObserver().Process;
            userService.LoginSucessEvent += new LogObserver().Process;
            if (!userService.Login("admin", "123456"))
            {
                Console.WriteLine("Login failed.");
            }

            Console.ReadLine();
        }
    }
}
