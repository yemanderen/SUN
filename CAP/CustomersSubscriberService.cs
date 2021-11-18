using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAP
{
    [CapSubscribe("customers")]
    public class CustomersSubscriberService : ICapSubscribe
    {
        [CapSubscribe("create", isPartial: true)]
        public void Create(Customer customer)
        {
        }
    }
}
