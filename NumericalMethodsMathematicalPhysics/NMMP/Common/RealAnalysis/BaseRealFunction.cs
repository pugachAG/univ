using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RealAnalysis
{
    public abstract class BaseRealFunction
    {
        public abstract double GetValue(double arg);

        public abstract BaseRealFunction GetNthFunctionalDerivative(int n);
    }


}
