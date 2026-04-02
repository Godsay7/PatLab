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
        public bool WasCleanedToday { get; set; } = false;
        public PhysicalStates State { get; set; } = PhysicalStates.Full;

        public event Action<Animal, PhysicalStates> StateChanged;
        public void ChangeState(PhysicalStates newState)
        {
            if (State == newState)
                return;

            State = newState;

            StateChanged?.Invoke(this, State);
        }

        public void Eat()
        {
            if (State == PhysicalStates.Dead)
            {
                Console.WriteLine($"The {NameAnimal} is dead and cannot eat.");
                return;
            }

            if (MealsToday >= 5)
            {
                Console.WriteLine($"The {NameAnimal} has already eaten the maximum number of times today.");
                return;
            }

            if (HourSinceLastMeal < 3)
            {
                Console.WriteLine($"The {NameAnimal} is not hungry yet (last meal was {HourSinceLastMeal} hours ago).");
                return;
            }

            MealsToday += 1;
            HourSinceLastMeal = 0;
            ChangeState(PhysicalStates.Full);
        }

        public void PassTime(int hours, bool isEndOfDay)
        {
            if (State == PhysicalStates.Dead)
                return;

            if (hours <= 0)
                return;

            HourSinceLastMeal += hours;

            if (HourSinceLastMeal > 8 && State != PhysicalStates.Dead)
            {
                ChangeState(PhysicalStates.HungryTired);
            }

            if (isEndOfDay)
            {
                if (MealsToday == 0 || MealsToday > 5)
                {
                    ChangeState(PhysicalStates.Dead);
                }
                else if (State != PhysicalStates.Dead)
                {
                    MealsToday = 0;
                    WasCleanedToday = false;
                }
            }
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
