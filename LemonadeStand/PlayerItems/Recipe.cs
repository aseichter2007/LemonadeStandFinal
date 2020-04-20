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
        public Recipe()
        {
            amountOfLemons = 0 ;
            amountOfSugarCubes = 0 ;
            amountOfIceCubes =0;
            pricePerCup =0;
        }
        public void SetIngredients(int lemons, int sugar, int ice, int price)
        {
            amountOfLemons = lemons;
            amountOfSugarCubes = sugar;
            amountOfIceCubes = ice;
            pricePerCup = price * 0.01;
        }
    }
}
