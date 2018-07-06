using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    enum CalcType
    {
        Add = 1,
        Subtract = 2
    }
    class CalcFactory
    {
        public ICalcInterFace CerateCalcIntance(CalcType calcType)
        {
            ICalcInterFace CalcInterFace = null;
            switch (calcType)
            {
                case CalcType.Add: CalcInterFace = new Add(); break;
                case CalcType.Subtract: CalcInterFace = new Subtract(); break;
                default: break;
            }
            return CalcInterFace;
        }
    }
}
