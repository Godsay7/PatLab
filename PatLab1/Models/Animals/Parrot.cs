using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Capabilities;

namespace PatLab1.Models.Animals
{
    public class Parrot : Animal, IFlyable, ISingable
    {
        public Parrot()
        {
            NameAnimal = "Parrot";
            Eyes = 2;
            Paws = 2;
            Wings = 2;
        }
        public void Fly()
        {
            if (HourSinceLastMeal > 8)
            {
                Console.WriteLine("The parrot wants to eat and can't fly.");
                Walk();
            }
            else
                Console.WriteLine("Parrot is flying");
        }

        public void Sing()
        {
            if (HourSinceLastMeal > 8)
            {
                Console.WriteLine("The parrot wants to eat and can't sing.");
            }
            else
                Console.WriteLine("Parrot is singing");
        }
    }
}
