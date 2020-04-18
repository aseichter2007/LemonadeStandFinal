using LemonadeStand.DaysandWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Game
    {
        Player player;
        List<Day> days;
        int currentDay;
        Random random;
        int difficulty;
        int duration=7;
        public Game()
        {
            days = new List<Day>();
            difficulty=3;
            random = new Random();
            GenerateDays(random, difficulty,duration);
        }
        void GenerateDays(Random random, int difficulty, int duration)
        {
            for (int i = 0; i < duration+1; i++)
            {
                Day day = new Day(random, difficulty);
                days.Add(day);
            }

        }
        int InputCheck(string input)
        {
            int output=0;

            return output;
        }
    }
}
