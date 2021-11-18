using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSample02
{
    class WeatherData
    {
        /// <summary>
        /// 气温
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        public string humility { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        public string pressure { get; set; }
    }
}
