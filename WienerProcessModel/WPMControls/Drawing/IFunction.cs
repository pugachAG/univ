using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMControls.Drawing
{
    public interface IFunction
    {
        double GetValue(double x);
    }

    public class FuncWrapper<T, TResult> : IFunction
    {
        private Func<T, TResult> func;

        public FuncWrapper(Func<T, TResult> func)
        {
            this.func = func;
        }


        public double GetValue(double x)
        {
            //ugly hack, boxing, exceptions etc...
            T arg = (T)(object)x;
            double result = (double)(object)func(arg);
            return result;
        }
    }
}
