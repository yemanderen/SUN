using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAP
{
    public interface ISubscriberService
    {
        void CheckReceivedMessage(DateTime datetime);
    }

    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("xxx.services.show.time")]
        public void CheckReceivedMessage(DateTime datetime)
        {
        }

        [CapSubscribe("xxx.services.show.time", Group = "group1")]
        public void ShowTime1(DateTime datetime)
        {
        }

        [CapSubscribe("xxx.services.show.time", Group = "group2")]
        public void ShowTime2(DateTime datetime)
        {
        }
    }
}
