using LemonadeStand.PlayerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand
{
    class Store
    {
        // member variables (HAS A)
        private double pricePerLemon;
        public double Lemon 
        {
            get => pricePerLemon;
        }
        private double pricePerSugarCube;
        public double Sugar
        {
            get => pricePerSugarCube;
        }
        private double pricePerIceCube;
        public double Ice
        {
            get => pricePerIceCube;
        }
        private double pricePerCup;
        public double Cup
        {
            get => pricePerCup;
        }

        // constructor (SPAWNER)
        public Store()
        {
            pricePerLemon = .4;
            pricePerSugarCube = .1;
            pricePerIceCube = .01;
            pricePerCup = .08;
        }

        // member methods (CAN DO)
        public void SellLemons(Player player)
        {
            int lemonsToPurchase = UserInterface.GetNumberOfItems("lemons");
            double transactionAmount = CalculateTransactionAmount(lemonsToPurchase, pricePerLemon);
            if (player.wallet.Money >= transactionAmount)
            {
                player.wallet.PayMoneyForItems(transactionAmount);
                player.inventory.AddLemonsToInventory(lemonsToPurchase);
            }
        }

        public void SellSugarCubes(Player player)
        {
            int sugarToPurchase = UserInterface.GetNumberOfItems("sugar");
            double transactionAmount = CalculateTransactionAmount(sugarToPurchase, pricePerSugarCube);
            if (player.wallet.Money >= transactionAmount)
            {
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddSugarCubesToInventory(sugarToPurchase);
            }
        }

        public void SellIceCubes(Player player)
        {
            int iceCubesToPurchase = UserInterface.GetNumberOfItems("ice cubes");
            double transactionAmount = CalculateTransactionAmount(iceCubesToPurchase, pricePerIceCube);
            if (player.wallet.Money >= transactionAmount)
            {
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddIceCubesToInventory(iceCubesToPurchase);
            }
        }

        public void SellCups(Player player)
        {
            int cupsToPurchase = UserInterface.GetNumberOfItems("cups");
            double transactionAmount = CalculateTransactionAmount(cupsToPurchase, pricePerCup);
            if (player.wallet.Money >= transactionAmount)
            {
                PerformTransaction(player.wallet, transactionAmount);
                player.inventory.AddCupsToInventory(cupsToPurchase);
            }
        }

        private double CalculateTransactionAmount(int itemCount, double itemPricePerUnit)
        {
            double transactionAmount = itemCount * itemPricePerUnit;
            return transactionAmount;
        }

        private void PerformTransaction(Wallet wallet, double transactionAmount)
        {
            wallet.PayMoneyForItems(transactionAmount);
        }
        public void SelltoAI(Player player,Random random) 
        {
            bool lemon = false;
            bool sugar = false;
            bool cups = false;
            bool ice = false;
            int count;
            do
            {
                if (!lemon)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(1, 40), pricePerLemon);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddLemonsToInventory(count);
                        lemon = true;
                    }
                }
                if (!sugar)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(1, 40), pricePerSugarCube);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddSugarCubesToInventory(count);
                        lemon = true;
                    }
                }
                if (!cups)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(1, 100), pricePerCup);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddCupsToInventory(count);
                        lemon = true;
                    }
                }
                if (!ice)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(1, 200), pricePerCup);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddCupsToInventory(count);
                        lemon = true;
                    }
                }
            } while (!lemon&&!sugar&&!cups&&!ice);
            
}
    }
}
