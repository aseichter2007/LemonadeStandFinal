using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace LemonadeStand
{
    static class SaveGame
    {
        private static string name;
        private static double money;
        private static double totalprofit;
        private static int currentday;
        private static int duration;
        private static int difficulty;
        private static int recipeLemon;
        private static int recipeSugar;
        private static int recipeIce;
        private static int lemons;
        private static int ice;
        private static int cups;
        private static int sugar;
        private static double price;
        static List<Player> playerlist;


        
        public static bool LoadSave(Player player)
        {
            string line;
            bool load=false;
            string currentDirectory = Directory.GetCurrentDirectory();
            //if (File.Exists(currentDirectory + "\\save.txt"))
            //{
            //StreamReader streamReader = new StreamReader(currentDirectory + "\\save.txt");

            //commented for amazon s3 funtionality

            AmazonUpload amazonUpload = new AmazonUpload();

                Stream stream = amazonUpload.getMyFilesFromS3("aseichter", "etc", "save.txt");
                StreamReader streamReader = new StreamReader(stream);
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Contains("username==" + player.name))
                    {
                        load = UserInterface.LoadSave();
                        if (load)
                        {
                            currentday = int.Parse(streamReader.ReadLine());
                            duration = int.Parse(streamReader.ReadLine());
                            difficulty = int.Parse(streamReader.ReadLine());
                            money = double.Parse(streamReader.ReadLine());
                            totalprofit = double.Parse(streamReader.ReadLine());
                            recipeLemon = int.Parse(streamReader.ReadLine());
                            recipeIce = int.Parse(streamReader.ReadLine());
                            recipeSugar = int.Parse(streamReader.ReadLine());
                            lemons = int.Parse(streamReader.ReadLine());
                            sugar = int.Parse(streamReader.ReadLine());
                            cups = int.Parse(streamReader.ReadLine());
                            ice = int.Parse(streamReader.ReadLine());
                            price = double.Parse(streamReader.ReadLine());

                        }

                    }

                }
                stream.Close();
                streamReader.Close();
                

           // }

            if (load)
            {
                
                player.wallet.PayMoneyForItems(20);
                player.wallet.Money = money;
                player.wallet.totalProfit = totalprofit;
                player.currentday = currentday;
                player.difficulty = difficulty;
                player.duration = duration;
                player.SetRecipe(recipeLemon, recipeSugar, recipeIce, int.Parse((price*100).ToString()));
                player.inventory.AddLemonsToInventory(lemons);
                player.inventory.AddSugarCubesToInventory(sugar);
                player.inventory.AddCupsToInventory(cups);
                player.inventory.AddIceCubesToInventory(ice);
            }
            return load;
        }
        public static void Preservesaves(Player saveplayer)
        {
            playerlist = new List<Player>();

            string line;
            string currentDirectory = Directory.GetCurrentDirectory();

            //commented for amazon s3 funtionality
            //StreamReader streamReader = new StreamReader(currentDirectory + "/save.txt");
            AmazonUpload amazonUpload = new AmazonUpload();
            Stream stream = amazonUpload.getMyFilesFromS3("aseichter", "etc", "save.txt");
            StreamReader streamReader = new StreamReader(stream);
            while ((line = streamReader.ReadLine()) != null)
            {
                if (line.Contains("username=="))
                {
                    Player player = new Player();
                    if (line!= "username=="+ saveplayer.name)
                    {
                        name = line.Substring(10);
                        currentday = int.Parse(streamReader.ReadLine());
                        duration = int.Parse(streamReader.ReadLine());
                        difficulty = int.Parse(streamReader.ReadLine());
                        money = double.Parse(streamReader.ReadLine());
                        totalprofit = double.Parse(streamReader.ReadLine());
                        recipeLemon = int.Parse(streamReader.ReadLine());
                        recipeIce = int.Parse(streamReader.ReadLine());
                        recipeSugar = int.Parse(streamReader.ReadLine());
                        lemons = int.Parse(streamReader.ReadLine());
                        sugar = int.Parse(streamReader.ReadLine());
                        cups = int.Parse(streamReader.ReadLine());
                        ice = int.Parse(streamReader.ReadLine());
                        price = double.Parse(streamReader.ReadLine());

                        player.name = name;
                        player.wallet.PayMoneyForItems(20);
                        player.wallet.Money = money;
                        player.wallet.totalProfit = totalprofit;
                        player.currentday = currentday;
                        player.difficulty = difficulty;
                        player.duration = duration;
                        player.SetRecipe(recipeLemon, recipeSugar, recipeIce, int.Parse((price * 100).ToString()));
                        player.inventory.AddLemonsToInventory(lemons);
                        player.inventory.AddSugarCubesToInventory(sugar);
                        player.inventory.AddCupsToInventory(cups);
                        player.inventory.AddIceCubesToInventory(ice);
                        playerlist.Add(player);
                    }
                }
            }
            stream.Close();
            streamReader.Close();
            playerlist.Add(saveplayer);
        }

        public static void SavePlayer(Player saveplayer,int currentday, int duration, int difficulty)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            //if (File.Exists(currentDirectory + "\\save.txt"))
            //{
                Preservesaves(saveplayer);
            //}
            //commented for amazon s3 funtionality
            //else
            //{
            //    playerlist = new List<Player>();
            //    playerlist.Add(saveplayer);
            //}
            StreamWriter filestream= new StreamWriter(currentDirectory+"\\save.txt",false);

            foreach (Player player in playerlist)
            {
                filestream.WriteLine("username==" + player.name);
                filestream.WriteLine(currentday);
                filestream.WriteLine(duration);
                filestream.WriteLine(difficulty);
                filestream.WriteLine(player.wallet.Money);
                filestream.WriteLine(player.wallet.totalProfit);
                filestream.WriteLine(player.recipe.amountOfLemons);
                filestream.WriteLine(player.recipe.amountOfSugarCubes);
                filestream.WriteLine(player.recipe.amountOfIceCubes);
                filestream.WriteLine(player.inventory.lemons.Count);
                filestream.WriteLine(player.inventory.sugarCubes.Count);
                filestream.WriteLine(player.inventory.cups.Count);
                filestream.WriteLine(player.inventory.iceCubes.Count);
                filestream.WriteLine(player.recipe.pricePerCup);
            }
            filestream.Close();
            AmazonUpload amazonUpload = new AmazonUpload();
            amazonUpload.sendMyFileToS3(currentDirectory + "\\save.txt", "aseichter", "etc", "save.txt");

        }
    }
}
