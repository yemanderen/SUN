using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSample01
{
    public class LocationReporter2 : IObserver<LocationTracker2>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(LocationTracker2 value)
        {
            throw new NotImplementedException();
        }
    }
}
