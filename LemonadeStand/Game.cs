using LemonadeStand.Customers;
using LemonadeStand.DaysandWeather;
using LemonadeStand.PlayerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Game
    {
        List<Player> PlayerList;
        Player player;
        Store store;
        List<Day> days;
        int currentDay;
        Random random;
        int difficulty;
        int duration = 7;
        public Game()
        {
            PlayerList = new List<Player>();
            days = new List<Day>();
            store = new Store();
            random = new Random();
        }
        public void RunGame()
        {
            GameSetup();
            currentDay = 0;
            do
            {
                foreach (Player gameplayer in PlayerList)
                {
                    Turn(player);
                }
                currentDay++;
            } while (currentDay<+duration);
        }
        void GameSetup()
        {
            
            int[] players = UserInterface.GameSetup();
            duration = players[2];
            difficulty = players[3];
            GenerateDays(random, difficulty, duration);
            days[0].weather.GetForecast(days, random);
            for (int i = 0; i < players[0]; i++)
            {
                player = new Player();
                UserInterface.PlayerSetup(player);
                PlayerList.Add(player);
            }
            for (int i = 0; i < players[1]; i++)
            {
                player = new AI();
                player.name = days[0].customers[0].GetName(random);
            }           
            
        }

        void Turn(Player player)
        {
            if (player.human)
            {
             UserInterface.BetweenDayStatusChoice(player, 0, days[currentDay], store);
            }
            else
            {
                player.AITurn(random,store,player);
            }           
            double daysProfit =CustomersDrink(days[currentDay], player.recipe,player.human);
            player.wallet.Money = daysProfit;
            Console.WriteLine("today {1} made {0}", daysProfit.ToString("c"),player.name);
            IceMelts(player);
            Console.WriteLine("press enter to continue");
            Console.ReadLine();
            Console.Clear();

        }
        void IceMelts(Player player)
        {
            Console.WriteLine(player.inventory.iceCubes.Count + "melted");
            player.inventory.iceCubes.Clear();
        }
        double CustomersDrink(Day day, Recipe recipe, bool human)
        {
            double daysPofit = 0;
            bool breaker = false;
            foreach (Customer customer in day.customers)
            {                
                    int[] bought = customer.BuyLemonade(player.wallet, recipe, day.weather, human);
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
                        bool haveIngredients = player.FillPitcher(human);
                        if (haveIngredients)
                        {
                            player.pitcher.cupsLeftInPitcher--;
                            daysPofit += recipe.pricePerCup;
                        }
                        else
                        {
                            if (human)
                            {
                                Console.WriteLine("the other customers went home empty handed.");
                            }
                            breaker = true;
                            break;
                        }
                    }

                    if (human&&bought[1]==1)
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
                if (breaker)
                {
                    break;
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
       
    }
}
