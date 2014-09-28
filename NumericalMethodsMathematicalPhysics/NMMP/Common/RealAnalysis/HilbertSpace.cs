using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RealAnalysis
{
    public class HilbertSpace
    {
        private double a;
        private double b;

        public HilbertSpace(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double GetScalarProduct(BaseRealFunction f1, BaseRealFunction f2)
        {
            return IntegralCalculator.RiemannIntegral(f1.Mult(f2), a, b);
        }
    }
}
