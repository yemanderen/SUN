using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternSample
{
    public class LogObserver : ILoginSucessObserver
    {
        public void Process(LoginMessageModel message)
        {
            Console.WriteLine("LogObserver");
        }

    }
}
