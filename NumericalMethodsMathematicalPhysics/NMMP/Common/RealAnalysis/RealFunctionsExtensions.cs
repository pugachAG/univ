using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RealAnalysis
{
    public static class RealFunctionsExtensions
    {
        public static BaseRealFunction ToRealFunction(this Func<double, double> func)
        {
            return new FuncRealFunction(func);
        }
    }
}
