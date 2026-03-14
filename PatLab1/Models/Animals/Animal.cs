using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Capabilities;
using PatLab1.Models.States;

namespace PatLab1.Models.Animals
{
    public abstract class Animal : ICrawlable, IWalkable
    {
        public int id { get; set; }
        public string NameAnimal { get; protected set; } = string.Empty;
        public int Eyes { get; protected set; }
        public int Paws { get; protected set; }
        public int Wings { get; protected set; }
        public int HourSinceLastMeal { get; set; } = 0;
        public bool IsHappy { get; set; } = true;
        public int MealsToday { get; set; } = 0;
        public PhysicalStates State { get; set; } = PhysicalStates.Full;

        public event Action<PhysicalStates> StateChanged;
        public void ChangeState(PhysicalStates newState)
        {
            if (State == newState)
                return;

            State = newState;

            StateChanged?.Invoke(State);
        }

        public void Eat()
        {
            if (MealsToday < 5 && HourSinceLastMeal > 3)
            {
                MealsToday += 1;
                HourSinceLastMeal = 0;
            }
            else
                Console.WriteLine($"The {NameAnimal} is not hungry");


        }
        public void PassTime()
        {
            if (true) { }//якась логіка
            ChangeState(PhysicalStates.HungryTired);
            //теж якась логіка
            ChangeState(PhysicalStates.Dead);
        }

        public void Craw()
        {
            Console.WriteLine($"{NameAnimal} is crawling");
        }

        public void Walk()
        {
            Console.WriteLine($"{NameAnimal} is walking");
        }
    }
}
