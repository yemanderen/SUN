using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSample02
{
    class CurrentConditionDisplay : WeatherDisplayBase
    {
        public override void OnNext(WeatherData value)
        {
            Console.WriteLine("------------------");
            Console.WriteLine("当前天气状况板");
            Console.WriteLine(string.Format("温度：{0}\n湿度：{1}\n气压：{2}",
                value.temperature, value.humility, value.pressure));
        }
    }
}
