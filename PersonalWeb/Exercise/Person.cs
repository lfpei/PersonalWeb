using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    class Person : ITest
    {
        public void guandeng()
        {
            System.Windows.Forms.MessageBox.Show("renguandeng");
        }
        public void kaideng()
        {
            System.Windows.Forms.MessageBox.Show("开灯");
        }
        public string guandeng2(string canshu)
        {
            return canshu;
        }
    }
    class Product
    {
        public void zhixing(ITest test)
        {
            test.guandeng2(string.Empty);
        }
    }
}
