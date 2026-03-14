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
        Home() { MaxAnimals = 1; }
        public void Clean(Animal animal) { /*if (!animal.IsHappy) */ animal.IsHappy = true; }
        public void Feed(Animal animal) { animal.Eat(); }
    }
}