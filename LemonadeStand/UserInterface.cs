using LemonadeStand.DaysandWeather;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    static class UserInterface
    {
        public static int GetNumberOfItems(string itemsToGet)
        {
            bool userInputIsAnInteger = false;
            int quantityOfItem = -1;

            while (!userInputIsAnInteger || quantityOfItem < 0)
            {
                Console.WriteLine("How many " + itemsToGet + " would you like to buy?");
                Console.WriteLine("Please enter a positive integer (or 0 to cancel):");

                userInputIsAnInteger = int.TryParse(Console.ReadLine(), out quantityOfItem);
            }

            return quantityOfItem;
        }
        static void GoToStore(Player player, Store store)
        {
            bool stay = true;
            do
            {
                string money;
                Console.WriteLine();
                Console.WriteLine("Your mom took you to the store with her, kicking asnd screaming.");
                Console.WriteLine("You have " + player.wallet.Money.ToString("c") + "," + player.inventory.lemons.Count + " lemons, " + player.inventory.sugarCubes.Count + " sugar cubes, " + player.inventory.iceCubes.Count + " ice cubes, and " + player.inventory.cups.Count + " cups.");
                Console.WriteLine("Your mom will only let you buy fom this list:");
                Console.WriteLine("Lemons are {0} each." , money =store.Lemon.ToString("c"));
                Console.WriteLine("Sugar Cubes are {0} each." , money =store.Sugar.ToString("c"));
                Console.WriteLine("Ice Cubes are {0} each" , money = store.Ice.ToString("c"));
                Console.WriteLine("Cups are {0} each." , money =store.Cup.ToString("c"));
                Console.WriteLine("________________________________");
                Console.WriteLine();
                Console.WriteLine("What would you like to buy? type exit to go home.");
                string input = Console.ReadLine();
                bool check = ChooseStoreItem(input.Trim(), player,store);
                Console.Clear();
                stay = CheckExit(input);

                if (!check && stay)
                {
                    Console.WriteLine();
                    Console.WriteLine("Your mom wont let you buy that. Press enter to retry. ");
                    Console.ReadLine();
                    Console.Clear();
                } 
            }
            while (stay);
        }
        static bool ChooseStoreItem(string input, Player player,Store store)
        {
            bool output = false;
            switch (input)
            {
                case "lemons":
                case "Lemons":
                case "l":
                case "L":
                    store.SellLemons(player);
                        output = true;
                        break;
                case "Sugar":
                case "sugar":
                case "Sugar Cubes":
                case "Sugar cubes":
                case "sugar Cubes":
                case "sugar cubes":
                case "sugarcubes":
                case "S":
                case "s":
                    store.SellSugarCubes(player);
                        output = true;
                    break;
                case "Ice":
                case "ice":
                case "Ice Cubes":
                case "Ice cubes":
                case "ice Cubes":
                case "ice cubes":
                case "I":
                case "i":
                    store.SellIceCubes(player);
                        output = true;
                    break;
                case "cups":
                case "Cups":
                case "cup":
                case "Cup":
                case "C":
                case "c":
                    store.SellCups(player);
                        output = true;
                    break;
            }
            return output;
        }
        public static int[] GameSetup()
        {
            bool safe = false;
            string humans;
            string robots;
            string duration;
            string difficulty;
            do
            {
                Console.WriteLine("How many human players?  0-99");
                humans = Console.ReadLine();
                safe = InputSanitizer(humans,2);
            } while (!safe);
            safe = false;
            do
            {
                Console.WriteLine("How many ai players?  0-99");
                robots = Console.ReadLine();
                safe = InputSanitizer(robots,2);

            } while (!safe);
            safe = false;
            do
            {
                Console.WriteLine("How many days will you play? 5-999");
                duration = Console.ReadLine();
                safe = InputSanitizer(robots, 3);
                if (int.Parse(duration) < 5)
                {
                    safe = false;
                }
            } while (!safe);
            bool stay = true;
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Choose difficulty. 3=easy, 2=medium, 1=hard.");
                string hard = Console.ReadLine();
                safe = InputSanitizer(hard, 1);
                if (safe && int.Parse(hard) < 4)
                {
                    difficulty = hard;
                }
                else
                {
                    safe = false;
                }
            }
            while (!safe);
            int[] output = new int[4] { int.Parse(humans), int.Parse(robots),int.Parse(duration), int.Parse(duration) };
            Console.Clear();
            return output;
        }
        public static void PlayerSetup(Player player)
        {
            Console.WriteLine();
            Console.WriteLine("enter player name");
            player.name = Console.ReadLine();            
        }
        public static void BetweenDayStatusChoice(Player player, int currentDay, Day day, Store store)
        {
            bool safe= false;
            string input="";
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Goodmorning {0}",player.name);
                Console.WriteLine("you have some time before opening your lemonade stand, what would you like to do before day {0} begins?",currentDay);
                Console.WriteLine("You have "+player.wallet.Money.ToString("c")+ "," + player.inventory.lemons.Count + " lemons, " + player.inventory.sugarCubes.Count + " sugar cubes, " + player.inventory.iceCubes.Count + " ice cubes, and " + player.inventory.cups.Count + " cups.");
                Console.WriteLine("1. Send your mom to the store.");
                Console.WriteLine("2. Check the weather forecast.");
                Console.WriteLine("3. Change your lemonade recipe.");
                Console.WriteLine("4. Nothing. Go get that cash from strangers.");
                input = Console.ReadLine();
                safe = InputSanitizer(input,1);
                if (safe)
                {
                    Console.Clear();
                    safe =ChoiceChoose(input.Trim(), player,currentDay,day, store);
                }

                if (safe == false&& input.Trim()!="4")
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Improper choice. press enter try again.");
                    Console.ReadLine();                    
                }
            }
            while (safe==false||input.Trim()!="4");

        }
        static bool InputSanitizer(string input, int length)
        {
            string bad = "ABCDEFGHIJKLMNOPQRSTUVWKYZ";
            char[] badCharachters = bad.ToLower().ToCharArray();
            bool output = true;
            foreach (char c in badCharachters)
            {
                if (input.ToLower().Contains(c))
                {
                    output = false;
                }
            }
            if (output&&input!=""&&double.Parse(input)<1&&double.Parse(input)>0)
            {
                input = (double.Parse(input) *100).ToString();
            }
            if (input.Trim().Length > length || input == "")
            {
                output = false;
            }
            
            return output;
        }

        static bool ChoiceChoose(string input,Player player, int currentDay, Day day,Store store)
        {
            bool output = false;
            Console.Clear();
            switch (input)
            {
                case "1":
                    GoToStore(player, store);
                    output = true;
                    break;
                case "2":
                    CheckForecast(day);
                    output = true;
                    break;
                case "3":
                    SetRecipe(player);
                    output = true;
                    break;
                case "4":
                    output = true;
                    break;
            }
            return output;
        }
        static void SetRecipe(Player player)
        {
            bool stay = true;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Current recipe");
                Console.WriteLine(player.recipe.amountOfLemons + " Lemons per pitcher");
                Console.WriteLine(player.recipe.amountOfSugarCubes + " sugar cubes per pitcher");
                Console.WriteLine(player.recipe.amountOfIceCubes + " ice cubes per cup");
                Console.WriteLine(player.recipe.pricePerCup.ToString("c") + " per cup");
                Console.WriteLine();
                Console.WriteLine("____________________________________________)");
                Console.WriteLine();
                Console.WriteLine("Which part of the recipe would you like to change?  type exit to go back.");
                string input = Console.ReadLine();
                bool check = ChooseRecipeEdit(input.Trim(),player);                
                stay = CheckExit(input);
                Console.Clear();
                if (!check)
                {
                    Console.WriteLine("nothing was changed.");
                }
            }
            while (stay);
        }
        static bool CheckExit(string input)
        {
            bool output = true;
            switch (input)
            {
                case "exit":
                case "Exit":
                case "leave":
                case "no":
                case "e":
                case "back":
                case "go back":
                case "b":
                    output = false;                
                    break;
            }
            return output;
        }
        static bool ChooseRecipeEdit(string input, Player player)
        {
            bool output = false;
            switch (input)
            {
                case "lemons":
                case "Lemons":
                case "l":
                case "L":
                        output = RecipeSetLemons(player);
                    break;
                case "Sugar":
                case "sugar":
                case "Sugar Cubes":
                case "Sugar cubes":
                case "sugar Cubes":
                case "sugar cubes":
                case "sugarcubes":
                case "S":
                case "s":
                        output =RecipeSetSugarCubes(player);
                    break;
                case "Ice":
                case "ice":
                case "Ice Cubes":
                case "Ice cubes":
                case "ice Cubes":
                case "ice cubes":
                case "I":
                case "i":
                        output = RecipeSetIceCubes(player);
                    break;
                case "price":
                case "Price":
                case "Price per cup":
                case "Price per Cup":
                case "price per cup":
                case "price Per Cup":
                case "price Per cup":
                case "P":
                case "p":
                        output = RecipeSetPrice(player);
                    break;
            }
            return output;
        }
        static bool RecipeSetLemons(Player player)
            {
            bool output = false;
            Console.WriteLine("the old recipe has {0} lemons.", player.recipe.amountOfLemons);
            Console.WriteLine();
            Console.WriteLine("how many lemons would you like in today's lemonade? Press enter to ");
            string input = Console.ReadLine().Trim();
            bool check = InputSanitizer(input, 2);
            if (check)
            {
                player.recipe.amountOfLemons = int.Parse(input);
                output = true;
            }

            return output;
            }
        static bool RecipeSetSugarCubes(Player player)
        {
            bool output = false;
            Console.WriteLine("the old recipe has {0} sugar cubes.", player.recipe.amountOfSugarCubes);
            Console.WriteLine();
            Console.WriteLine("how many sugar cubes would you like in today's lemonade? /n Press enter to change your mind and go back.");
            string input = Console.ReadLine().Trim();
            bool check = InputSanitizer(input, 2);
            if (check)
            {
                player.recipe.amountOfSugarCubes = int.Parse(input);
                output = true;
            }

            return output;
        }
        static bool RecipeSetIceCubes(Player player)
        {
            bool output = false;
            Console.WriteLine("the old recipe has {0} ice cubes per cup.", player.recipe.amountOfSugarCubes);
            Console.WriteLine();
            Console.WriteLine("how many ice cubes per cup would you like to serve with today's lemonade? /n Press enter to change your mind and go back.");
            string input = Console.ReadLine().Trim();
            bool check = InputSanitizer(input, 2);
            if (check)
            {
                player.recipe.amountOfIceCubes = int.Parse(input);
                output = true;
            }
                        
            return output;
        }
        static bool RecipeSetPrice(Player player)
            {
                bool output = false;
                Console.WriteLine("the old recipe costs {0}.", player.recipe.pricePerCup.ToString("c"));
                Console.WriteLine();
                Console.WriteLine("how many cents will you charge for today's lemonade?  /n Press enter to change your mind and go back.");
                string input = Console.ReadLine().Trim();                
                bool check = InputSanitizer(input, 2);
                if (check)
                {
                     if (input != "" && double.Parse(input) < 1 && double.Parse(input) > 0)
                     {
                        input = (double.Parse(input) * 100).ToString();
                     }
                     player.recipe.pricePerCup = double.Parse(input)*0.01;
                     output = true;
                }

                return output;
            }       
        public static void CheckForecast(Day day)
        {   
            
            List<string> label = new List<string>() { "Today ", "Tomorrow ", "The next day ", "The next day ", "The next day ", "The next day ", "The next day " };
            int count = 0;
            Console.WriteLine("The 7 day weather forecast is :");
            foreach (string[] forecast in day.forecast)
            {
                Console.WriteLine(label[count] + "the weather may be " + forecast[0] + " and " + forecast[1] + " degrees.");
                count++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("press enter to go back");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
