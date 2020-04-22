using LemonadeStand.Customers;
using LemonadeStand.DaysandWeather;
using LemonadeStand.PlayerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
            do
            {
                foreach (Player gameplayer in PlayerList)
                {
                    Turn(gameplayer);
                }
                currentDay++;
            } while (currentDay <= duration);
            LeaderBoard();
        }
        void LeaderBoard()
        {
            foreach (Player finishplayer in PlayerList)
            {
                UserInterface.Leaderboard(finishplayer);
            }
        }
        void GameSetup()
        {

            int[] players = UserInterface.GameSetup();
            duration = players[2];
            difficulty = players[3];
            currentDay = 0;
            for (int i = 0; i < players[0]; i++)
            {
                player = new Player();
                UserInterface.PlayerSetup(player);
                bool load = SaveGame.LoadSave(player);
                PlayerList.Add(player);
                if (load)
                {
                    currentDay = player.currentday;
                    difficulty = player.difficulty;
                    duration = player.duration;
                }
            }
            GenerateDays(random, difficulty, duration);
            days[0].weather.GetForecast(days, random);
            for (int i = 0; i < players[1]; i++)
            {
                player = new AI();
                player.name = days[0].customers[0].GetName(random);
                PlayerList.Add(player);
            }

        }

        void Turn(Player player)
        {

            if (player.human)
            {
                UserInterface.BetweenDayStatusChoice(player, currentDay, days[currentDay], store, duration,difficulty);
            }
            else
            {
                player.AITurn(random, store, player);
                UserInterface.AIdentify(player);
            }
            double daysProfit = CustomersDrink(player, days[currentDay], player.human);
            //player.wallet.Money = daysProfit;  legacy.
            IceMelts(player);
            UserInterface.PressEnter();
            Console.ReadLine();
            Console.Clear();

        }
        void IceMelts(Player player)
        {
            UserInterface.IceMelts(player);
            player.inventory.iceCubes.Clear();
        }
        double CustomersDrink(Player player, Day day, bool human)
        {
            double daysPofit = 0;
            bool breaker = false;
            int cupssold = 0;
            int pitchersMade = 0;
            int[] startinventory = DayStartInventory(player.inventory);
            foreach (Customer customer in day.customers)
            {
                int[] bought = customer.BuyLemonade(player, day.weather, human);
                UserInterface.CustomersDrink(bought[0], customer);
                for (int i = 0; i < bought[0]; i++)
                {                    
                        daysPofit += player.recipe.pricePerCup;
                        cupssold++;
                }
                if (human && bought[1] == 1)
                {
                    foreach (Customer customer1 in day.customers)
                    {
                        if (customer1.type == "cop")
                        {
                            UserInterface.CopCaught(customer1.name, customer.name);
                            break;
                        }
                    }
                }
                if (breaker)
                {
                    break;
                }
            }
            double dayscost = OperatingCost(startinventory, player.inventory);
            UserInterface.SoldToday(daysPofit,dayscost, cupssold);
            player.wallet.totalProfit += daysPofit - dayscost;
            return daysPofit;
        }
        int[] DayStartInventory(Inventory inventory)
        {
            int lemons = inventory.lemons.Count;
            int sugar = inventory.sugarCubes.Count;
            int cups = inventory.cups.Count;
            int ice = inventory.iceCubes.Count;
            int[] output = new int[4] { lemons, sugar, cups, ice };
            return output;
        }
        void GenerateDays(Random random, int difficulty, int duration)
        {
            for (int i = 0; i < duration + 1; i++)
            {
                Day day = new Day(random, difficulty);
                days.Add(day);
            }
        }
        double OperatingCost(int [] startinventory, Inventory inventory)
        {
            double output = 0;
            output += (startinventory[0] - inventory.lemons.Count) * store.Lemon;
            output += (startinventory[1] - inventory.lemons.Count)* store.Sugar;
            output += (startinventory[2] - inventory.cups.Count) * store.Cup; ;
            output += (startinventory[3] - inventory.iceCubes.Count) * store.Ice;
            return output;


        }

    }
}
