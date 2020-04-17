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
        public Game()
        {
            difficulty = 1;
            random = new Random();
            GenerateDays(random, 1);
        }
        void GenerateDays(Random random, int difficulty)
        {
            Day day = new Day(random, difficulty);

        }
    }
}
