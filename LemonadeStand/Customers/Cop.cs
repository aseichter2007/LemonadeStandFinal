using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.Customers
{
    class Cop : Customer
    {
        string name;
        List<string> copnames;
        public Cop(Random random)
        {
            name = GetName(random);
        }
        public override string GetName(Random random)
        {
            string output = copnames[random.Next(0, copnames.Count - 1)];
            return output;
        }
    }
    
}
