using Common.RealAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Lab2
{
    public static class InputData2
    {
        public const int PointsCount = 100;
        public const double a = 0;
        public const double b = 3;

        public static FuncRealFunction k { get { return new FuncRealFunction(x => x + 1); } }
        public static FuncRealFunction q { get { return new FuncRealFunction(x => x + 1); } }

        public static double alpha1 { get { return 1; } }
        public static double alpha2 { get { return 3; } }
        public static double mu1 { get { return getMu(alpha1, a, -1); } }
        public static double mu2 { get { return getMu(alpha2, b, 1); } }




        public static FuncRealFunction f
        {
            get
            {
                return new FuncRealFunction(x => 2 * Math.Sin(x) * (x + 1) - Math.Cos(x));
            }
        }

        public static BaseRealFunction L(BaseRealFunction func)
        {
            BaseRealFunction functionalDerivative = func.GetNthFunctionalDerivative(1);
            return functionalDerivative.Mult(k).GetNthFunctionalDerivative(1).Minus().Sum(
                q.Mult(func));
        }

        public static BaseRealFunction Solution
        {
            get
            {
                return new FuncRealFunction(x => Math.Sin(x));
            }
        }

        private static double getMu(double alpha, double x, int coef)
        {
            BaseRealFunction functionalDerivative = Solution.GetNthFunctionalDerivative(1);
            return coef * functionalDerivative.GetValue(x) * k.GetValue(x) + alpha * Solution.GetValue(x);
        }
    }
}
