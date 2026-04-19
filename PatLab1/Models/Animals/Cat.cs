using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Capabilities;

namespace PatLab1.Models.Animals
{
    public class Cat : Animal, IFastMovable, ISoundable
    {
        public Cat() 
        {
            NameAnimal = "Cat";
            Eyes = 2;
            Paws = 4;
            Wings = 0;
        }

        public void MakeSound()
        {
            Console.WriteLine("*Meow!*");
        }
    }
}
