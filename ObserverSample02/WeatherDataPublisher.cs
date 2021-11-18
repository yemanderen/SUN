using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSample02
{
    class WeatherDataPublisher : IObservable<WeatherData>
    {
        List<IObserver<WeatherData>> observers = new List<IObserver<WeatherData>>();
        /// <summary>
        /// 订阅主题，将观察者添加到列表中
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<WeatherData> observer)
        {
            observers.Add(observer);
            return new Unsubscribe(this.observers, observer);
        }
        /// <summary>
        /// 取消订阅类
        /// </summary>
        private class Unsubscribe : IDisposable
        {
            List<IObserver<WeatherData>> observers;
            IObserver<WeatherData> observer;
            public Unsubscribe(List<IObserver<WeatherData>> observers
            , IObserver<WeatherData> observer)
            {
                this.observer = observer;
                this.observers = observers;
            }

            public void Dispose()
            {
                if (this.observers != null)
                {
                    this.observers.Remove(observer);
                }
            }
        }
        /// <summary>
        /// 通知已订阅的观察者
        /// </summary>
        /// <param name="weatherData"></param>
        private void Notify(WeatherData weatherData)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(weatherData);
            }
        }
        /// <summary>
        /// 接收最新的天气数据
        /// </summary>
        /// <param name="weatherData"></param>
        public void ReciveNewData(WeatherData weatherData)
        {
            Notify(weatherData);
        }
    }
}
