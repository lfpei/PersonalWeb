using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    class Class1
    {
        A a = new A();
        public bool Get()
        {
            return a.Get();
        }
    }


    class A
    {
        public bool Get()
        {
            return true;
        }
    }

    class B
    {
        public bool Get(string ss)
        {
            return true;
        }
    }

    class Client
    {
        public void getResult()
        {
            Class1 aaaa = new Class1();
            aaaa.Get();
        }
    }
}
