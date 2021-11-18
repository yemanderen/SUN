using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP
{
    public class Customer
    {
        public virtual int UserID { get; set; }
        public virtual string Username { get; set; }
        public virtual string LoginIP { get; set; }
    }
}
