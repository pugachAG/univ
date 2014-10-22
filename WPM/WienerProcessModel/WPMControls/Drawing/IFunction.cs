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
            T arg = (T)Convert.ChangeType(x, typeof(T));
            double result = (double)Convert.ChangeType(func(arg), typeof(double));
            return result;
        }
    }
}
