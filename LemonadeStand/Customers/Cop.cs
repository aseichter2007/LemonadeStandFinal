using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.Customers
{
    class Cop : Customer
    {
        public Cop(Random random):base(random)
        {
            type = "cop";
        }
        public override string GetName(Random random)
        {
            names = new List<string>() { "Officer Dick", "Officer Johnny 5","Officer Doofy", "Officer Smiley", "Officer Downe", "Officer Willie", "Officer Schwartz", "Officer Farva", "Officer Rabbit", "Special Agent Osso" };

            string output = names[random.Next(1, names.Count-1)];
            return output;
        }
        
    }
    
}
