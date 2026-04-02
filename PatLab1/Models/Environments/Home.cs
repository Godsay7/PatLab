using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Animals;

namespace PatLab1.Models.Environments
{
    public class Home : Environment
    {
        public Home()
        {
            MaxAnimals = 1;
            Name = "Home";
        }
        public void Clean(Animal animal)
        {
            if (animal == null) return;

            animal.IsHappy = true;
            animal.WasCleanedToday = true;
        }

        public void Feed(Animal animal)
        {
            if (animal == null) return;
            animal.Eat();
        }
    }
}