using Common.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RealAnalysis
{
    public class UniformGridRealFunction : BaseRealFunction
    {
        double a;
        double b;
        int n;
        List<double> vals = new List<double>();

        private FuncRealFunction funcWrapper;

        public UniformGridRealFunction(Matrix<double> source, double a, double b, int n)
        {
            for(int i = 0; i < source.RowsCount; i++)
            {
                vals.Add(source[i, 0]);
            }
            this.a = a;
            this.b = b;
            this.n = n;

            funcWrapper = new FuncRealFunction(x => GetValue(x));
        }

        public override double GetValue(double arg)
        {
            double h = (b - a) / n;
            if(arg > b)
                return vals.Last();
            if(arg < a)
                return vals.First();
            int index = (int)((arg - a) / h);
            if(index == vals.Count - 1)
                return vals[index];
            else
            {
                double lambda = 1 - (arg - (index * h + a)) / h;
                return vals[index] *lambda + vals[index + 1] * (1 - lambda);
            }
        }

        public override BaseRealFunction GetNthFunctionalDerivative(int n)
        {
            return funcWrapper.GetNthFunctionalDerivative(n);
        }

        public override BaseRealFunction Mult(BaseRealFunction f)
        {
            return funcWrapper.Mult(f);
        }

        public override BaseRealFunction Sum(BaseRealFunction f)
        {
            return funcWrapper.Sum(f);
        }

        public override BaseRealFunction Minus()
        {
            return funcWrapper.Minus();
        }
    }
}
