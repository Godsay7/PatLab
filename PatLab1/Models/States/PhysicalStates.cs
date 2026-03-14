using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatLab1.Models.States
{
    public enum PhysicalStates
    {
        Full, //щойно або <8 годин назад поїла, в цей день вже померти не може, якщо їла 5 разів за день стан буде до кінця дня
        HungryTired, //починає день з такого стану, їла >8 годин назад, обмежуються певні активні дії
        Dead //стан що викликається в кінці дня якщо за 24 години тварина ніразу не їла
    }
}
