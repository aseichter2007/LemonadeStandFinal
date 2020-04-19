using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.PlayerItems
{
    class Recipe
    {
        public int amountOfLemons;
        public int amountOfSugarCubes;
        public int amountOfIceCubes;
        public double pricePerCup;
        public Recipe(int lemons,int sugar, int ice, int price)
        {
            amountOfLemons = lemons;
            amountOfSugarCubes = sugar;
            amountOfIceCubes = ice;
            pricePerCup = price;
        }
    }
}
