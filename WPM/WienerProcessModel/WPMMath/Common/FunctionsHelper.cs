using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMMath.Common
{
    public static class FunctionsHelper
    {
        public static DiscreteFunction GetSupremumFunction(DiscreteFunction function)
        {
            DiscreteFunction result = new DiscreteFunction();
            decimal val = decimal.MinValue;
            foreach(var p in function.Data)
            {
                val = Math.Max(val, p.Value);
                result.AddValue(p.Argument, val);
            }
            return result;
        }

        public static DiscreteFunction GetIntegralFunction(Func<decimal, decimal> func, DiscreteFunction gridFunction)
        {
            DiscreteFunction result = new DiscreteFunction();
            var data = gridFunction.Data.ToList();
            decimal prev = 0;
            for(int i = 1; i < data.Count; i++)
            {
                decimal x1 = data[i - 1].Argument;
                decimal x2 = data[i].Argument;
                decimal dx = data[i].Value - data[i - 1].Value;
                decimal df = func(x2) - func(x1);

                decimal cur = dx * df;
                prev += cur;
                result.AddValue(x2, prev);
            }
            return result;
        }
    }
}
