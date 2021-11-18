using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternSample
{
    public class MyClient : IMyClient
    {
        public void GetOtherOne()
        {
            Console.WriteLine("MyClient GetOtherOne");
        }

        public void GetOtherTwo()
        {
            Console.WriteLine("MyClient GetOtherTwo");
        }

    }
}
