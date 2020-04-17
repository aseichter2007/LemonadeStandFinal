using LemonadeStand.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Player
    {
        // member variables (HAS A)
        public Inventory inventory;
        public Wallet wallet;
        public Recipe recipe;
        Pitcher pitcher;

        // constructor (SPAWNER)
        public Player()
        {
            inventory = new Inventory();
            wallet = new Wallet();
            Console.WriteLine("player exists");
        }

        // member methods (CAN DO)
    }
}
