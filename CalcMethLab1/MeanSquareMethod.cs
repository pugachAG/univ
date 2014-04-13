using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethLab
{
    public class MeanSquareMethodContinuous : AproximationMethodBase
    {
        protected override double CalcScalarProduct(Func<double, double> f, Func<double, double> g)
        {
            return MathHelper.ScalarProduct(f, g, base.a, base.b);
        }

        protected override double CalcScalarProduct(Func<double, double> f, Func<double, double> g, double from, double to)
        {
            return MathHelper.ScalarProduct(f, g, from, to);
        }
    }

    public class MeanSquareMethodDicrete : AproximationMethodBase
    {
        public MeanSquareMethodDicrete()
        {
            base.isDiscrete = true;
        }

        protected override double CalcScalarProduct(Func<double, double> f, Func<double, double> g)
        {
            return MathHelper.SquareDifference(f, g, base.a, base.b, base.m);
        }

        protected override double CalcScalarProduct(Func<double, double> f, Func<double, double> g, double from, double to)
        {
            return MathHelper.SquareDifference(f, g, from, to, base.m);
        }
    }
}
