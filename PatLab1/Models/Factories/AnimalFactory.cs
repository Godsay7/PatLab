using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Animals;

namespace PatLab1.Models.Factories
{
    public static class AnimalFactory
    {
        private static int _nextAnimalId = 1;

        public static Animal CreateAnimal(string choice)
        {
            Animal newAnimal = null;

            switch (choice)
            {
                case "1":
                    newAnimal = new Cat();
                    break;
                case "2":
                    newAnimal = new Dog();
                    break;
                case "3":
                    newAnimal = new Parrot();
                    break;
                case "4":
                    newAnimal = new Spider();
                    break;
                default:
                    return null;
            }

            if (newAnimal != null)
            {
                newAnimal.id = _nextAnimalId++;
            }

            return newAnimal;
        }
    }
}
