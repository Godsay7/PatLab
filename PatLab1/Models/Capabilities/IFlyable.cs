using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatLab1.Models.Capabilities
{
    public interface IFlyable
    {
        void Fly()
        {
            Console.WriteLine("The animal is flying.");
        }
    }
}
