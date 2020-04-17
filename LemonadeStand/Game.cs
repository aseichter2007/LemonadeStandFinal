using LemonadeStand.DaysandWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Game
    {
        Player player;
        List<Day> days;
        int currentDay;
        public Game()
        {
            player = new Player();
        }
    }
}
