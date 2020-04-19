using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.Customers
{
        class Customer
    {
        //list names has to be public so I can get at it fom the cop child.
        public List<string> names;
        public string name;
        public int thirst;
        public int sweettooth;
        public Random random;
        public string type;
        public   Customer(Random random)
        {
            this.random = random;
            names = new List<string>() {"ET","Eddie Mercury","William Ryker","Oprah Winfrey","Tyrion Lannister","John Snow","Mr. Clean", "The Undertaker", "Dwayne Johnson","Jack Black","your mom","Bambi","Casper the Ghost" ,"Rip Van Winkle","Macguyver","John Lukepikard","Freddie Mercury","Michael Jackson" ,"Rick Sanchez","Emelia Earhart","Elvis Presley","Harry Potter","Ron Swanson","Hellen Kellar","Bruce Willis","Pepper Potts","Bruce Wayne","Michael Terrill","Michael Heinisch","Brett Johnson", "Charles King","David Legrange","Nevin Seibel" };

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
