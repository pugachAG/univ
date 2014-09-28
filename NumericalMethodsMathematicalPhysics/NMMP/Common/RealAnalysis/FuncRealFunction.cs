using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RealAnalysis
{
    public class FuncRealFunction: BaseRealFunction
    {
        public const decimal Epsilon = 1e-3M;

        private Func<double, double> func = null;

        public FuncRealFunction(Func<double, double> func)
        {
            this.func = func;
        }

        public override double GetValue(double arg)
        {
            return func(arg);
        }

        public override BaseRealFunction GetNthFunctionalDerivative(int n)
        {
            Func<decimal, decimal> baseFunc = new Func<decimal, decimal>(x => (decimal)func((double)x));
            Func<decimal, decimal> nthDerivedFunc = GetNthDerivedFunc(baseFunc, n);
            return new FuncRealFunction(arg => (double)nthDerivedFunc((decimal)arg));
        }

        private Func<decimal, decimal> GetNthDerivedFunc(Func<decimal, decimal> baseFunc, int n)
        {
            Func<decimal, decimal> current = baseFunc;
            for (int i = 0; i < n; i++)
            {
                Func<decimal, decimal> prev = current;
                current = new Func<decimal, decimal>(arg => (prev(arg + Epsilon) - prev(arg)) / Epsilon);
            }
            return current;
        }

        public static FuncRealFunction operator *(FuncRealFunction f1, FuncRealFunction f2)
        {
            return new FuncRealFunction(arg => f1.GetValue(arg) * f2.GetValue(arg));
        }

        public static FuncRealFunction operator +(FuncRealFunction f1, FuncRealFunction f2)
        {
            return new FuncRealFunction(arg => f1.GetValue(arg) + f2.GetValue(arg));
        }
    }
}
