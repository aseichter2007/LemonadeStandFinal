using LemonadeStand.DaysandWeather;
using LemonadeStand.PlayerItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand.Customers
{
    class Customer
    {
        protected List<string> names;
        public string name;
        protected int thirst;
        protected int sweettooth;
        public Random random;
        public string type;
        protected double money;
        protected int lemonfresh;

        public Customer(Random random)
        {
            names = new List<string>() { "ET", "Austin Power's Dad", "Robin Williams", "Bruce Wayne","Kanye East", "Marshal Mathers", "Dave Chappele", "Light Yagami", "Walt Disney", "Spyro", "Forrest Gump", "Antonio Banderas", "Bryan Boitano", "Eric Cartman", "Kenny McCormic", "Lindsay Lohan", "Dr. Evil"  , "Eddie Mercury", "William Ryker", "Oprah Winfrey", "Tyrion Lannister", "John Snow", "Mr. Clean", "The Undertaker", "Dwayne Johnson", "Jack Black", "your mom", "Bambi", "Casper the Ghost", "Rip Van Winkle", "Macguyver", "John Lukepikard", "Freddie Mercury", "Michael Jackson", "Rick Sanchez", "Emelia Earhart", "Elvis Presley", "Harry Potter", "Ron Swanson", "Hellen Kellar", "Bruce Willis", "Pepper Potts", "Bruce Wayne", "Michael Terrill", "Michael Heinisch", "Brett Johnson", "Charles King", "David Legrange", "Nevin Seibel" };
            sweettooth = GetSweet(random);
            thirst = GetThirst(random);
            name = GetName(random);
            type = "customer";
            money = GetMoney(random);
            lemonfresh = GetSweet(random);

        }
        int GetSweet(Random random)
        {
            int output = random.Next(0, 10);
            return output;
        }
        int GetThirst(Random random)
        {
            int output = random.Next(20, 500);
            return output;
        }
        public virtual string GetName(Random random)
        {
            string output = names[random.Next(0, names.Count - 1)];
            return output;
        }
        double GetMoney(Random random)
        {
            double output = random.Next(0, 10);
            return output;
        }
        public virtual int[] BuyLemonade(Wallet wallet, Recipe recipe, Weather weather,bool human)
        {
            int [] output = new int[2] { 0, 0 };
            while (money > recipe.pricePerCup&&thirst>0)
            {
                bool thirsty = LemonadeCraving(recipe, weather, human);
                if (thirsty)
                {                    
                    money -= recipe.pricePerCup;
                    wallet.Money = recipe.pricePerCup;
                    thirst -= 80;
                    output[0]++;
                }
                else
                {
                    break;
                }
            }
            return output;
        }
        protected bool LemonadeCraving(Recipe recipe, Weather weather,bool human)
        {
            int truethirst = (thirst + weather.temperature*6) / 2;
            int idealIce = (weather.temperature -20) / 12;
            int flavor = HorrbleMess(idealIce, recipe);
            double craving = flavor*0.6 * truethirst ;
            bool buyADrink = craving > 200;
            if (human&&flavor==0)
            {
                Console.WriteLine(name + " doesn't like your lemonade.");
            }
            return buyADrink;
        }
        protected int HorrbleMess(int idealIce, Recipe recipe)
        {
            int flavor = 0;
            if (sweettooth == recipe.amountOfSugarCubes && lemonfresh == recipe.amountOfLemons && idealIce == recipe.amountOfIceCubes)
            {
                flavor = 10;
            }
            else if ((sweettooth == recipe.amountOfSugarCubes && lemonfresh == recipe.amountOfLemons) || (lemonfresh == recipe.amountOfLemons && idealIce == recipe.amountOfIceCubes) || (sweettooth == recipe.amountOfSugarCubes && idealIce == recipe.amountOfIceCubes))
            {
                flavor = 5;
            }
            else if (sweettooth == recipe.amountOfSugarCubes || lemonfresh == recipe.amountOfLemons || idealIce == recipe.amountOfIceCubes)
            {
                flavor = 3;
            }

            if (sweettooth == recipe.amountOfSugarCubes - 1 || sweettooth == recipe.amountOfSugarCubes + 1)
            {
                flavor += 2;

            }

            if (sweettooth == recipe.amountOfSugarCubes - 2 | sweettooth == recipe.amountOfSugarCubes + 2)
            {
                flavor++;
            }

            if (sweettooth - recipe.amountOfSugarCubes > 0)
            {
                if (sweettooth - recipe.amountOfSugarCubes > 5)
                {
                    flavor -= 2;

                }
                else if (sweettooth - recipe.amountOfSugarCubes > 3)
                {
                    flavor--;
                }

            }
            else
            {
                if (recipe.amountOfSugarCubes - sweettooth > 5)
                {
                    flavor -= 2;
                }
                else if (recipe.amountOfSugarCubes - sweettooth > 3)
                {
                    flavor--;
                }
            }

            if (lemonfresh == recipe.amountOfLemons - 1 || lemonfresh == recipe.amountOfLemons + 1)
            {
                flavor += 2;

            }

            if (lemonfresh == recipe.amountOfLemons - 2 | lemonfresh == recipe.amountOfLemons + 2)
            {
                flavor++;
            }

            if (lemonfresh - recipe.amountOfLemons > 0)
            {
                if (lemonfresh - recipe.amountOfLemons > 5)
                {
                    flavor -= 2;

                }
                else if (lemonfresh - recipe.amountOfLemons > 3)
                {
                    flavor--;
                }

            }
            else
            {
                if (recipe.amountOfLemons - lemonfresh > 5)
                {
                    flavor -= 2;
                }
                else if (recipe.amountOfLemons - lemonfresh > 3)
                {
                    flavor--;
                }
            }

            if (idealIce == recipe.amountOfIceCubes - 1 || idealIce == recipe.amountOfIceCubes + 1)
            {
                flavor += 2;

            }

            if (idealIce == recipe.amountOfIceCubes - 2 | idealIce == recipe.amountOfIceCubes + 2)
            {
                flavor+=1;
            }

            if (idealIce - recipe.amountOfIceCubes > 0)
            {
                if (idealIce - recipe.amountOfIceCubes > 5)
                {
                    flavor -= 1;

                }
            }
            else
            {
                if (recipe.amountOfIceCubes - idealIce > 5)
                {
                    flavor -= 1;
                }
            }

            if (recipe.pricePerCup>0.50)
            {
                flavor -= 9;
            }
            else if (recipe.pricePerCup>0.45)
            {
                flavor -= 7;
            }
            else if (recipe.pricePerCup>0.40)
            {
                flavor -= 5;
            }
            else if (recipe.pricePerCup>0.35)
            {
                flavor -= 4;
            }
            else if (recipe.pricePerCup>0.30)
            {
                flavor -= 3;
            }
            else if (recipe.pricePerCup>0.25)
            {
                flavor -= 2;
            }
            else if (recipe.pricePerCup>0.20)
            {
                flavor -= 1;
            }
            else if (recipe.pricePerCup>0.15)
            {
                
            }
            else if (recipe.pricePerCup>0.10)
            {
                flavor += 1;
            }
            else if (recipe.pricePerCup>0.05)
            {
                flavor += 2;
            }
            else
            {
                flavor += 5;
            }

            if (flavor<0)
            {
                flavor = 0;
            }

            return flavor;
        }

    }   
}
