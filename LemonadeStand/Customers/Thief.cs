using LemonadeStand.DaysandWeather;
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
        public override int[] BuyLemonade(Player player, Weather weather,bool human)
        {
            int[] output = new int[2] { 0, 0 };
            while (thirst > 0)
            {
                bool thirsty = LemonadeCraving(player.recipe, weather, human);
                if (player.pitcher.cupsLeftInPitcher<1)
                {
                    thirsty = player.FillPitcher(human);
                }
                if (thirsty)
                {
                    player.pitcher.cupsLeftInPitcher--;
                    money -= player.recipe.pricePerCup;
                    if (money > 0)
                    {
                        player.wallet.Money = player.recipe.pricePerCup;
                    }
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
