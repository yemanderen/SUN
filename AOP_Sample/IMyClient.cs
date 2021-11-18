using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternSample
{
    public interface IMyClient
    {
        //public virtual int UserID { get; set; }
        //public virtual string Username { get; set; }
        //public virtual string LoginIP { get; set; }

        public void GetOtherOne();
        void GetOtherTwo();
    }
}
