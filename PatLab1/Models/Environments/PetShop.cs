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
        public PetShop() { MaxAnimals = 15; }
        public void CleanOne(Animal animal) { /*if (!animal.IsHappy) */ animal.IsHappy = true; }
        public void FeedOne(Animal animal) { animal.Eat(); }
        public void CleanAll()
        {
            foreach (Animal el in ListOfAnimals)
            {
                if (el.IsHappy != true) el.IsHappy = true; 
            }
        }
        public void FeedAll() { foreach (Animal el in ListOfAnimals) el.Eat(); }
    }
}