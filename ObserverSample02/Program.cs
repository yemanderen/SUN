using System;

namespace ObserverSample02
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherDataPublisher publisher = new WeatherDataPublisher();
            CurrentConditionDisplay currentDisplay = new CurrentConditionDisplay();
            StatisticsConditionDisplay statisticsDisplay
            = new StatisticsConditionDisplay();
            //订阅当前天气展示板
            IDisposable currentDisplayUnsubscriber =
            publisher.Subscribe(currentDisplay);
            //订阅气温统计展示板
            IDisposable statisticsDisplayUnsubscriber =
            publisher.Subscribe(statisticsDisplay);

            for (int i = 0; ; i++)
            {
                WeatherData weatherData = new WeatherData();
                Console.WriteLine("请输入温度,湿度,压力");
                string input = Console.ReadLine();
                var array = input.Split(',');
                weatherData.temperature = array[0];
                weatherData.humility = array[1];
                weatherData.pressure = array[2];
                Console.WriteLine("");
                //将输入的新的天气数据传给天气数据发布器
                publisher.ReciveNewData(weatherData);
                Console.WriteLine("=============================");
            }
        }
    }
}
