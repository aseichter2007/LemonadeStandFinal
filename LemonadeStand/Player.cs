using LemonadeStand.PlayerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Player
    {
        // member variables (HAS A)
        public Inventory inventory;
        public Wallet wallet;
        public Recipe recipe;
        public Pitcher pitcher;
        public string name;
        public int currentday;
        public int duration;
        public int difficulty;
        public bool human;

        // constructor (SPAWNER)
        public Player()
        {
            inventory = new Inventory();
            wallet = new Wallet();
            recipe = new Recipe();
            pitcher = new Pitcher();
            human = true;
        }

        // member methods (CAN DO)
        public bool FillPitcher(bool human)
        {
            bool output = true;
            if (inventory.lemons.Count > recipe.amountOfLemons && inventory.sugarCubes.Count > recipe.amountOfSugarCubes && inventory.iceCubes.Count > recipe.amountOfIceCubes * 10 && inventory.cups.Count > 10)
            {
                inventory.lemons.RemoveRange(0, recipe.amountOfLemons);
                inventory.sugarCubes.RemoveRange(0, recipe.amountOfSugarCubes);
                inventory.iceCubes.RemoveRange(0, recipe.amountOfIceCubes * 10);
                inventory.cups.RemoveRange(0, 10);
                pitcher.cupsLeftInPitcher = 10;
            }
            else 
            { 
                output = false; 
            }
            if (human)
            {
                if (inventory.lemons.Count <= recipe.amountOfLemons)
                {
                    Console.WriteLine("You ran out of lemons.");

                }

                if (inventory.sugarCubes.Count <= recipe.amountOfSugarCubes)
                {
                    Console.WriteLine("You ran out of sugar cubes.");
                }

                if (inventory.iceCubes.Count <= recipe.amountOfIceCubes * 10)
                {
                    Console.WriteLine("You ran out of ice Cubes.");
                }

                if (inventory.cups.Count < 11)
                {
                    Console.WriteLine("You ran out of cups.");
                }
            }
            return output;
        }
        public void SetRecipe(int lemons, int sugar, int ice, int price)
        {
            recipe.SetIngredients(lemons, sugar, ice, price);
        }
        public virtual void AITurn(Random random, Store store, Player player) { }
    }
}
