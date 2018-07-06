using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    class Add : ICalcInterFace
    {
        public int Calculate(int a,int b)
        {
            return a + b;
        }
    }
}
