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
            type = "cop:";
        }
        public override string GetName(Random random)
        {
            string output = copnames[random.Next(1, copnames.Count-1)];
            return output;
        }
    }
    
}
