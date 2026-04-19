using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Capabilities;
using PatLab1.Models.States;

namespace PatLab1.Models.Animals
{
    public abstract class Animal : IMovable
    {
        public string NameAnimal { get; protected set; } = string.Empty;
        public int Eyes { get; protected set; }
        public int Paws { get; protected set; }
        public int Wings { get; protected set; }
        public int HourSinceLastMeal { get; protected set; } = 0;
        public int id { get; set; }
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

        public bool Eat()
        {
            if (MealsToday >= 5 || HourSinceLastMeal < 3)
            {
                return false;
            }

            MealsToday += 1;
            HourSinceLastMeal = 0;
            ChangeState(PhysicalStates.Full);
            return true;
        }

        public void PassTime(int hours, bool isEndOfDay)
        {
            if (State == PhysicalStates.Dead)
                return;

            if (hours <= 0)
                return;

            HourSinceLastMeal += hours;

            if (HourSinceLastMeal >= 8 && State != PhysicalStates.Dead && State != PhysicalStates.HungryTired)
            {
                ChangeState(PhysicalStates.HungryTired);
            }

            if (isEndOfDay)
            {
                if (MealsToday == 0 && HourSinceLastMeal >= 12)
                {
                    ChangeState(PhysicalStates.Dead);
                }
                else
                {
                    MealsToday = 0;
                    if (WasCleanedToday == false)
                    {
                        IsHappy = false;
                    }
                    else if (WasCleanedToday == true)
                    {
                        IsHappy = true;
                        WasCleanedToday = false;
                    }
                }
            }
        }
    }
}