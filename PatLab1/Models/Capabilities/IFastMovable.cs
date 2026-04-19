using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatLab1.Models.Capabilities
{
    public interface IFastMovable
    {
        void MoveFast() 
        {
            Console.WriteLine("The animal is running.");
        }
    }
}
