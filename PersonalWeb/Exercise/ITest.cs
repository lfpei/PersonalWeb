using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    interface ITest : ITest2
    {
        void guandeng();
        void kaideng();
    }
    interface ITest2 
    {
        string guandeng2(string canshu);
    }
}
