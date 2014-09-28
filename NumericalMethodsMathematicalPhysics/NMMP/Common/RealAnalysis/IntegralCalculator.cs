using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RealAnalysis
{
    public static class IntegralCalculator
    {
        const int PartitionsCount = 5000;
        const double Epsilon = 0.00001;

        public static double RiemannIntegral(BaseRealFunction function, double a, double b)
        {
            if (a > b)
                throw new ArgumentException("a > b");

            decimal result = 0;
            double dx = (b - a) / PartitionsCount;

            for (double x = a; x < b + Epsilon; x += dx)
            {
                double arg = x + dx / 2;
                double val = function.GetValue(arg);
                result += (decimal)val * (decimal)dx;
            }
            return (double)result;
        }
    }
}
