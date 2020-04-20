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
    class Duck:Customer
    {
        public Duck(Random random):base(random)
        {
            name = "a duck";
            type = "duck";            
        }
        public override int BuyLemonade(Wallet wallet,Recipe recipe,Weather weather)
        {
            int output = 0;
            Console.WriteLine(name + "asked if you have any grapes.");
            return output;
        }
    }

}
