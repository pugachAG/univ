using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMControls.Drawing
{
    public static class FunctionExtensions
    {
        public static IFunction ToIFunction<T, TResult>(this Func<T, TResult> func)
        {
            return new FuncWrapper<T, TResult>(func);
        }
    }
}
