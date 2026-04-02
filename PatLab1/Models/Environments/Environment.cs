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
        public string Name { get; protected set; } = string.Empty;

        public event Action<string> NotificationRaised;
        public void AddAnimal(Animal animal) 
        {
            if (MaxAnimals > ListOfAnimals.Count) 
            {
                ListOfAnimals.Add(animal);
                animal.StateChanged += OnAnimalStateChanged;
            }
            else Console.WriteLine("Limit of animals");
        }
        protected virtual void OnAnimalStateChanged(Animal animal, PhysicalStates newState)
        {
            if (newState == PhysicalStates.HungryTired) 
            {
                string msg = $"Animal {animal.NameAnimal} is tired or hungry!";
                NotificationRaised?.Invoke(msg);
            }
            else if (newState == PhysicalStates.Dead)
            {
                string msg = $"Your {animal.NameAnimal} was died.";
                NotificationRaised?.Invoke(msg);
                RemoveAnimal(animal);
            }
        }

        public void RemoveAnimal(Animal animal) 
        {
            animal.StateChanged -= OnAnimalStateChanged;
            ListOfAnimals.Remove(animal); 
        }
    }
}