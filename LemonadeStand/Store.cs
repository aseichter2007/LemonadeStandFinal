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
                if (!ice)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(20, 200), pricePerIceCube);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddIceCubesToInventory(count);
                        ice = true;
                    }
                }
                

                if (!lemon||player.inventory.lemons.Count>30)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(10, 30), pricePerLemon);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddLemonsToInventory(count);
                        lemon = true;
                    }
                }
                else
                {
                    lemon = true;
                }
                if (!sugar||player.inventory.sugarCubes.Count>30)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(10, 30), pricePerSugarCube);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddSugarCubesToInventory(count);
                        sugar = true;
                    }
                }
                else
                {
                    sugar = true;
                }
                
                if (!cups || player.inventory.cups.Count > 30)
                {
                    double transactionAmount = CalculateTransactionAmount(count = random.Next(10, 50), pricePerCup);
                    if (player.wallet.Money >= transactionAmount)
                    {
                        player.wallet.PayMoneyForItems(transactionAmount);
                        player.inventory.AddCupsToInventory(count);
                        cups = true;
                    }
                }
                else
                {
                    cups = true;
                }

                if (player.wallet.Money <pricePerLemon)
                {
                    lemon = true;
                    player.wallet.Money = 1.50;
                    player.inventory.AddLemonsToInventory(3);
                }
                if (player.wallet.Money < pricePerIceCube)
                {
                    ice = true;
                    player.wallet.Money = 1.50;
                    player.inventory.AddIceCubesToInventory(20);

                }
                if (player.wallet.Money < pricePerCup)
                {
                    cups = true;
                    player.wallet.Money = 1.50;
                    player.inventory.AddCupsToInventory(5);

                }
                if (player.wallet.Money < pricePerSugarCube)
                {
                    sugar = true;
                    player.wallet.Money = 1.50;
                    player.inventory.AddSugarCubesToInventory(5);
                }

            } while ((!lemon)&&(!sugar)&&(!cups)&&(!ice));
            
        }
    }
}
