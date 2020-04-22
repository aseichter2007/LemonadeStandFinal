using LemonadeStand.DaysandWeather;
using LemonadeStand.PlayerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.Customers
{
    class Duck : Customer
    {
        public Duck(Random random) : base(random)
        {
            name = "a duck";
            type = "duck";
        }
        public override int[] BuyLemonade(Player player,Weather weather,bool human)
        {
            int[] output = new int[] { 0, 0 };
            if (human)
            {
                UserInterface.WaddleWaddle(name);
            }
            return output;
        }
    }

}
