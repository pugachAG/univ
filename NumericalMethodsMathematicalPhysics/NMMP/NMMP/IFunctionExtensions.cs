using Common.RealAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMControls.Drawing;

namespace NMMP
{
    public static class IFunctionExtensions
    {
        public static IFunction ToIFunction(this BaseRealFunction function)
        {
            Func<double, double> func = new Func<double, double>(x =>
                function.GetValue(x));
            return new FuncWrapper<double, double>(func);
        }
    }
}
