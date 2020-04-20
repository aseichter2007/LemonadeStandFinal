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
        }
        void RunGame()
        {
            CustomersDrink(days[currentDay], player.recipe);
            currentDay++;
        }
        double CustomersDrink(Day day, Recipe recipe)
        {
            double daysPofit = 0;            
            foreach (Customer customer in day.customers)
            {                
                    int[] bought = customer.BuyLemonade(player.wallet, recipe, day.weather);
                    player.pitcher.cupsLeftInPitcher -= bought[0];
                for (int i = 0; i < bought[0]; i++)
                {
                    if (player.pitcher.cupsLeftInPitcher > 0)
                    {
                        player.pitcher.cupsLeftInPitcher--;
                        daysPofit += recipe.pricePerCup;
                    }
                    else
                    {
                        bool haveIngredients = player.FillPitcher();
                        if (haveIngredients)
                        {
                            player.pitcher.cupsLeftInPitcher--;
                            daysPofit += recipe.pricePerCup;
                        }
                        else
                        {
                            Console.WriteLine("the other customers went home empty handed.");
                            break;
                        }
                    }

                    if (bought[1]==1)
                    {
                        foreach (Customer customer1 in day.customers)
                        {
                            if (customer1.type=="cop")
                            {
                                Console.WriteLine(customer1.name + " caught " + customer.name + " stealing and took him to jail.");
                                break;
                            }
                        }
                    }
                }              
            }
            return daysPofit;
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
