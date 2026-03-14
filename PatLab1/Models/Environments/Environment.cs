using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Animals;
using PatLab1.Models.States;

namespace PatLab1.Models.Environments
{
    public abstract class Environment
    {
        public List<Animal> ListOfAnimals { get; set; } = new List<Animal>();
        public int MaxAnimals { get; protected set; }
        public void AddAnimal(Animal animal) 
        {
            if (MaxAnimals > ListOfAnimals.Count) 
            {
                ListOfAnimals.Add(animal);
                animal.StateChanged += OnAnimalStateChanged;
            }
            else Console.WriteLine("Limit of animals");
        }
        protected virtual void OnAnimalStateChanged(PhysicalStates newState)
        {
            if (newState == PhysicalStates.HungryTired) 
            {
                
            }
            else if (newState == PhysicalStates.Dead)
            {
                // Якщо тварина померла, Середовище має її прибрати
                // (Логіку видалення краще реалізувати обережно, щоб не зламати цикл, 
                // але концептуально це відбувається тут)
            }
        }

        public void RemoveAnimal(Animal animal) 
        {
            animal.StateChanged -= OnAnimalStateChanged;
            ListOfAnimals.Remove(animal); 
        }
    }
}