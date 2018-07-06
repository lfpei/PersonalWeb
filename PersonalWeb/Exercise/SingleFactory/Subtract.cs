using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    class Subtract : ICalcInterFace
    {
        public int Calculate(int a, int b)
        {
            if (a >= b)
            {
                return a - b;
            }
            else return b - a;
        }
    }
}
