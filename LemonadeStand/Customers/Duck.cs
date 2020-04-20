using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
