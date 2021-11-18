using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSample02
{
    abstract class WeatherDisplayBase : IObserver<WeatherData>
    {
        public virtual void OnCompleted()
        {
        }
        public virtual void OnError(Exception error)
        {
        }
        public abstract void OnNext(WeatherData value);
    }
}
