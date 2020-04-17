using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.Customers
{
        class Customer
    {
        List<string> names;
        public string name;
        public int thirst;
        public int sweettooth;
        public   Customer(Random random)
        {
            names = new List<string>() { "","" };
            sweettooth = GetSweet(random);
            thirst = GetThirst(random);
            name = GetName(random);
        }
        int GetSweet(Random random)
        {
            int output = random.Next(0, 10);
            return output;
        }
        int GetThirst(Random random)
        {
            int output = random.Next(0, 100);
            return output;

        }
        public virtual string GetName(Random random)
        {
            string output = names[random.Next(0, names.Count - 1)];
            return output;
        }

    }   
}
