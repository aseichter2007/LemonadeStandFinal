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
            weatherConditions = new List<string>() { "heat wave","sunny","sunny","partly cloudy","cloudy", "windy", "rainy","raining cats and dogs","storming","snowing", "thundersnow" };
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
            int temp = random.Next(65, 120);
            for (int i = 0; i < weatherConditions.Count-1; i++)
            {
                if (condition == weatherConditions[i])
                {
                    temp -= i * 7;
                    break;
                }
            }
            return temp;
        }
        public void GetForecast(List<Day> days, Random random)
        {

            for (int i = 0; i < days.Count; i++)
            {
                if (i+7 < days.Count)
                {
                    for (int j = i; j < i + 7; j++)
                    {

                        string doppler = days[j].weather.condition;
                        int radar = days[j].weather.temperature;
                        string weather = ForecasterCondition(doppler, random);
                        string temperature = ForecasterTemperature(radar, random);
                        string[] output = new string[2] { weather, temperature };
                        days[i].forecast.Add(output);
                    }
                }
                else
                {
                    int counter = 0;
                    int index = 7 - (days.Count-i);
                    int returnToDay = 0;
                    for (int j = i; j < days.Count; j++)
                    { 
                        string doppler = days[j].weather.condition;
                        int radar = days[j].weather.temperature;
                        string weather = ForecasterCondition(doppler, random);
                        string temperature = ForecasterTemperature(radar, random);
                        string[] output = new string[2] { weather, temperature };
                        days[i].forecast.Add(output);
                    }
                    for (counter = 0; counter < index; counter++)
                    {
                        string doppler = days[returnToDay].weather.condition;
                        int radar = days[returnToDay].weather.temperature;
                        string weather = ForecasterCondition(doppler, random);
                        string temperature = ForecasterTemperature(radar, random);
                        string[] output = new string[2] { weather, temperature };
                        days[i].forecast.Add(output);
                        returnToDay++;
                    }


                }
            }
        }
        string ForecasterCondition(string condition, Random random)
        {
            string output="";
            for (int i = 0; i < weatherConditions.Count; i++)
            {
                if (i == 0&&condition == weatherConditions[i])
                {
                    output = weatherConditions[random.Next(i, i + 2)];
                    break;
                }
                else if (i==weatherConditions.Count-1&&condition == weatherConditions[i])
                {
                    output = weatherConditions[random.Next(i-2, i)];
                    break;
                }
                else if (condition == weatherConditions[i])
                {
                    output = weatherConditions[random.Next(i - 1, i + 1)];
                    break;
                }
            }
            return output;
        }
        string ForecasterTemperature(int temperature, Random random)
        {
            string output = random.Next(temperature - 20, temperature + 20).ToString();
            return output;
        }

    }
}
