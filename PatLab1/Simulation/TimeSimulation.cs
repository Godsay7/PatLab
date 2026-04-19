using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;


namespace PatLab1.Simulation
{
    public class TimeSimulations
    {
        public int Hour { get; private set; } = 0;
        public int Day { get; private set; } = 0;
        
        public void UpdateTime(int hours)
        {
            Hour += hours;

            Day += Hour / 24;
            Hour %= 24;
        }
    }
}