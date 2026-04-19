using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Capabilities;

namespace PatLab1.Models.Animals
{
    public class Dog : Animal, IFastMovable, ISoundable
    {
        public Dog()
        {
            NameAnimal = "Dog";
            Eyes = 2;
            Paws = 4;
            Wings = 0;
        }
        
        public void MakeSound()
        {
            Console.WriteLine("*Rough rough*");
        }
    }
}
