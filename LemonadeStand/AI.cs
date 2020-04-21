using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class AI : Player
    {
        public AI()
        {
            setAI();
        }



           public void setAI()
        {
            human = false;
        }
        public void AITurn(Random random)
        {
            recipe.amountOfIceCubes=random.Next(10);
            recipe.amountOfLemons=random.Next(10);
            recipe.amountOfSugarCubes=random.Next(10);
            recipe.pricePerCup=random.NextDouble();






        }

    }
    

}
