﻿using LemonadeStand.DaysandWeather;
using LemonadeStand.PlayerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.Customers
{
    class Thief : Customer
    {
        public Thief(Random random):base(random)
        {
            type = "theif";
        }
        public override int[] BuyLemonade(Wallet wallet, Recipe recipe, Weather weather)
        {
            int[] output = new int[2] { 0, 0 };
            while (thirst > 0)
            {
                bool thirsty = LemonadeCraving(recipe, weather);
                if (thirsty)
                {
                    money -= recipe.pricePerCup;
                    wallet.Money = recipe.pricePerCup;
                    thirst -= 80;
                    output[0]++;
                    if (money<0)
                    {
                        output[1] = 1;
                    }
                }
                else
                {
                    break;
                }
                
            }
            return output;
        }
    }
}
