using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationSimilator
{
    public class WeatherRandomizer
    {

        // This is just to similate the weather information ONLY.
        static Random random = new Random();

        public static double RandomizeTemperatureValue()
        {
            return random.Next(-90, 60);
        }

        public static double RandomizeHumidityValue()
        {
            return random.Next(0, 100);
        }

        public static double RandomizePressureValue()
        {
            return random.Next(980, 1100);
        }

    }

}
