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
            double daysProfit = CustomersDrink(player.wallet, days[currentDay], player.recipe, player.human);
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
        double CustomersDrink(Wallet wallet, Day day, Recipe recipe, bool human)
        {
            double daysPofit = 0;
            bool breaker = false;
            int cupssold = 0;
            int pitchersMade = 0;
            foreach (Customer customer in day.customers)
            {
                int[] bought = customer.BuyLemonade(wallet, recipe, day.weather, human);
                UserInterface.CustomersDrink(bought[0], customer);
                for (int i = 0; i < bought[0]; i++)
                {
                    if (player.pitcher.cupsLeftInPitcher > 0)
                    {
                        player.pitcher.cupsLeftInPitcher--;
                        daysPofit += recipe.pricePerCup;
                        cupssold++;
                    }
                    else
                    {
                        bool haveIngredients = player.FillPitcher(human);
                        if (haveIngredients)
                        {
                            player.pitcher.cupsLeftInPitcher--;
                            daysPofit += recipe.pricePerCup;
                            cupssold++;
                            pitchersMade++;
                        }
                        else
                        {
                            if (human)
                            {
                                UserInterface.EmptyHanded();
                            }
                            breaker = true;
                            break;
                        }
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

                }
                if (breaker)
                {
                    break;
                }
            }
            double dayscost = OperatingCost(pitchersMade, recipe);
            UserInterface.SoldToday(daysPofit,dayscost, cupssold);
            wallet.totalProfit += daysPofit - dayscost;
            return daysPofit;
        }
        void GenerateDays(Random random, int difficulty, int duration)
        {
            for (int i = 0; i < duration + 1; i++)
            {
                Day day = new Day(random, difficulty);
                days.Add(day);
            }
        }
        double OperatingCost(int pitchers, Recipe recipe)
        {
            double output = 0;
            output += pitchers * recipe.amountOfLemons * store.Lemon;
            output += pitchers * recipe.amountOfIceCubes *10 * store.Ice;
            output += pitchers * recipe.amountOfSugarCubes * store.Sugar;
            output += pitchers * 10 * store.Cup;
            return output;


        }

    }
}
