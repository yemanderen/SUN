using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSample02
{
    class StatisticsConditionDisplay : WeatherDisplayBase
    {
        List<float> temperatures = new List<float>();
        public override void OnNext(WeatherData value)
        {
            float temperature;
            if (float.TryParse(value.temperature, out temperature))
            {
                temperatures.Add(temperature);
            }
            Console.WriteLine("------------------");
            Console.WriteLine("温度统计板");
            Console.WriteLine(string.Format("平均温度：{0}\n最高温度：{1}\n最低温度：{2}",
                temperatures.Average(), temperatures.Max(), temperatures.Min()));
        }
    }
}
