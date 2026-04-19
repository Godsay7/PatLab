using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Capabilities;

namespace PatLab1.Models.Animals
{
    public class Parrot : Animal, IFlyable, ISoundable
    {
        public Parrot()
        {
            NameAnimal = "Parrot";
            Eyes = 2;
            Paws = 2;
            Wings = 2;
        }
        public void MakeSound()
        {
            Console.WriteLine("Parrot is singing");
        }
    }
}
