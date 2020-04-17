using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.DaysandWeather
{
    class Weather
    {
        public string condition;
        public int temperature;
        //you want weatherConditions private but i dont want to to type it later for the comparator.
        public List<string> weatherConditions;
        public Weather(Random random)
        {
            weatherConditions = new List<string>() { "heat wave","sunny", "windy", "rainy","raining cats and dogs","storming","snowing", "thundersnow" };
            condition = GetConditions(random);
            temperature = GetTemp(random);
        }
        string GetConditions(Random random)
        {
            string output = weatherConditions[ random.Next(0, weatherConditions.Count - 1)];
            return output;
        }
        int GetTemp(Random random)
        {
            int temp = random.Next(40, 120);
            for (int i = 0; i < weatherConditions.Count-1; i++)
            {
                if (condition == weatherConditions[i])
                {
                    temp -= i * 15;
                    break;
                }
            }
            return temp;
        }
    }
}
