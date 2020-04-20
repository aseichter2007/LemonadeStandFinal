using LemonadeStand.Customers;
using LemonadeStand.DaysandWeather;
using LemonadeStand.PlayerItems;
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
        int duration = 7;
        public Game()
        {
            player = new Player();
            days = new List<Day>();
            difficulty = 3;
            random = new Random();
            GenerateDays(random, difficulty, duration);
            days[0].weather.GetForecast(days, random);
            CustomersDrink(days[0]);
        }
        void CustomersDrink(Day day)
        {
            Recipe recipe = new Recipe(2,2,7,20);
            Console.WriteLine(day.weather.condition + " and " + day.weather.temperature);
            foreach (Customer customer in day.customers)
            {
                Pitcher pitcher = new Pitcher();
                int bought =customer.BuyLemonade(player.wallet, recipe, day.weather, pitcher);
                Console.WriteLine(customer.name + " bought " + bought + " cups of lemonade");
            }
            
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
