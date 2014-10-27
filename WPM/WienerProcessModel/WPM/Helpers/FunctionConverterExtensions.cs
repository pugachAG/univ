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
            var data = discreteFunction.Data.ToList();
            Func<decimal, decimal> func = new Func<decimal,decimal>(x =>
            {
                int l = 0;
                int r = data.Count;
                while(l < r)
                {
                    int mid = (l + r) / 2;
                    if (data[mid].Argument > x)
                        r = mid - 1;
                    else
                        l = mid + 1;
                }
                l = Math.Min(data.Count - 1, l);
                return data[l].Value;
            });

            FuncWrapper<decimal, decimal> result = new FuncWrapper<decimal, decimal>(func);
            return result;
        }
    }
}
