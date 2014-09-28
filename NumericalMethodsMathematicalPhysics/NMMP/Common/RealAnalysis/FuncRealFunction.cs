using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RealAnalysis
{
    public class FuncRealFunction: IRealFunction
    {
        private Func<double, double> func = null;

        public FuncRealFunction(Func<double, double> func)
        {
            this.func = func;
        }


        public double GetValue(double arg)
        {
            return func(arg);
        }
    }
}
