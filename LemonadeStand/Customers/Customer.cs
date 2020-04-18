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
        //copnames is here because it cant instantiate in the cop child and GetName() proper like.
        public List<string> copnames;
        public string name;
        public int thirst;
        public int sweettooth;
        public Random random;
        public string type;
        public   Customer(Random random)
        {
            this.random = random;
            names = new List<string>() { "Michael Terrill","Michael Heinisch","Brett Johnson", "Charles King","David Legrange","nevin Seibel" };
            copnames = new List<string>() { "Officer Dick", "Officer Tom","Officer Cop","Officer Dan" };

            sweettooth = GetSweet(random);
            thirst = GetThirst(random);
            name = GetName(random);
            type = "customer";
            
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
