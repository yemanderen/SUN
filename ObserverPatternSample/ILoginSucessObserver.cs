using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternSample
{
    public interface ILoginSucessObserver
    {
        public void Process(LoginMessageModel message);
    }
}
