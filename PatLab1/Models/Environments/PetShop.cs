using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Animals;

namespace PatLab1.Models.Environments
{
    public class PetShop : Environment
    {
        public PetShop()
        {
            MaxAnimals = 15;
            Name = "Pet shop";
        }
        public void CleanOne(Animal animal)
        {
            if (animal == null) return;

            animal.IsHappy = true;
            animal.WasCleanedToday = true;
        }

        public void FeedOne(Animal animal)
        {
            if (animal == null) return;
            animal.Eat();
        }
        public void CleanAll()
        {
            foreach (Animal el in ListOfAnimals)
            {
                if (el.IsHappy != true)
                {
                    el.IsHappy = true;
                    el.WasCleanedToday = true;
                }
            }
        }
        public void FeedAll()
        {
            foreach (Animal el in ListOfAnimals)
            {
                el.Eat();
            }
        }
    }
}