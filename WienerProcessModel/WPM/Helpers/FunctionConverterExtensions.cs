using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMControls.Drawing;
using WPMMath.Common;

namespace WPM.Helpers
{
    public static class FunctionConverterExtensions
    {
        public static IFunction ConvertToIFunction(this DiscreteFunction discreteFunction)
        {
            Func<decimal, decimal> func = new Func<decimal,decimal>(x =>
            {
                foreach(var pair in discreteFunction.Data)
                {
                    if(pair.Argument >= x)
                        return pair.Value;
                }
                return 0;
            });

            FuncWrapper<decimal, decimal> result = new FuncWrapper<decimal, decimal>(func);
            return result;
        }
    }
}
