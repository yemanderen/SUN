using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSample01
{
    public class LocationTracker2 : IObservable<LocationTracker>
    {
        public IDisposable Subscribe(IObserver<LocationTracker> observer)
        {
            throw new NotImplementedException();
        }
    }
}
