using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatLab1.Models.Capabilities;

namespace PatLab1.Models.Animals
{
    public class Spider : Animal, IRunnable
    {
        public Spider() {
            NameAnimal = "Spider";
            Eyes = 8;
            Paws = 8;
            Wings = 0;
        }
        public void Run()
        {
            if (HourSinceLastMeal > 8)
            {
                Console.WriteLine("Spider want to eat and can't running");
                Walk();
            }
            else
                Console.WriteLine("Spider is running");
        }
    }
}
