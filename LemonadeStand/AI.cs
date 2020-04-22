﻿using System;
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
            difficulty = 3;
        }
        public override void AITurn(Random random,Store store, Player player)
        {
            recipe.amountOfIceCubes=random.Next(8);
            recipe.amountOfLemons=random.Next(8);
            recipe.amountOfSugarCubes=random.Next(8);
            recipe.pricePerCup=random.NextDouble()/2;
            store.SelltoAI(player,random);
        }

    }
    

}
