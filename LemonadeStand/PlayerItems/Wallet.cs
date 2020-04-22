using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.PlayerItems
{
    class Wallet
    {
        private double money;
        public double totalProfit;

        public double Money
        {
            get=> money;
            set => money = money + value;
        }

        public Wallet()
        {
            money = 20.00;
            totalProfit = 0;
        }

        public void PayMoneyForItems(double transactionAmount)
        {
            money -= transactionAmount;
        }
    }
}
