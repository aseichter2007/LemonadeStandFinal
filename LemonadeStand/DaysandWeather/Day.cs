using LemonadeStand.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.DaysandWeather
{
     class Day
    {
        public Weather weather;
        public List<Customer> customers;
        int hour;
        int count;
        public Day(Random random,int difficulty)
        {
            customers = new List<Customer>();
            count = GetCount(random,difficulty);
            GetCustomers(random,count);
        }
        void GetCustomers(Random random, int count)
        {
            Customer customer=new Customer(random);
            int customergen;
            for (int i = 0; i <= count; i++)
            {
                customergen = random.Next(0, 100);
                if (customergen > 0 && customergen < 6)
                {
                    customer = new Thief(random);
                }
                else if (customergen > 6 && customergen < 12)
                {
                    customer = new Cop(random);
                }
                else if (customergen == 0)
                {
                   customer = new Duck(random);
                }
                else
                {
                    customer = new Customer(random);
                }
                customers.Add(customer);
            }
        }
        int GetCount(Random random,int difficulty)
        {
            int output = random.Next(3, 10) * difficulty;
            return output;
        }
    }
}
