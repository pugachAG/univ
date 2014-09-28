using Common.RealAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Lab1
{
    public static class InputData
    {
        public const double a = 0;
        public const double b = 2;
        public const double b1 = 1;
        public const double b2 = 2;
        public const double b3 = 2;
        public const double c1 = 1;
        public const double c2 = 3;
        public const double c3 = 3;
        public const double d1 = 2;
        public const double d2 = 1;
        public const double d3 = 2;
        public const double k1 = 3;
        public const double k2 = 1;
        public const double p1 = 2;
        public const double p2 = 1;
        public const double q1 = 2;
        public const double q2 = 1;
        public const double a1 = 1;
        public const double a2 = 2;
        public const double a3 = 3;
        public const double a4 = 4;
        public const double n1 = 5;
        public const double n2 = 4;
        public const double n3 = 1;

        public static FuncRealFunction k { get { return new FuncRealFunction(x => b1 * Math.Pow(x, k1) + b2 * Math.Pow(x, k2) + b3);} }
        public static FuncRealFunction p { get { return new FuncRealFunction(x => c1 * Math.Pow(x, p1) + c2 * Math.Pow(x, p2) + c3); } }
        public static FuncRealFunction q { get { return new FuncRealFunction(x => d1 * Math.Pow(x, q1) + d2 * Math.Pow(x, q2) + d3); } }

        public static double alpha { get { return a1 * Math.Pow(a, n1) + a2 * Math.Pow(a, n2) + a3 * Math.Pow(a, n3) + a4; } }
        public static double beta { get { return a1 * n1 * Math.Pow(a, n1 - 1) + a2 * n2 * Math.Pow(a, n2 - 1) + a3 * n3 * Math.Pow(a, n3 - 1); } }
        public static double gamma { get { return a1 * Math.Pow(b, n1) + a2 * Math.Pow(b, n2) + a3 * Math.Pow(b, n3) + a4; } }
        public static double delta { get { return -(a1 * n1 * Math.Pow(b, n1 - 1) + a2 * n2 * Math.Pow(b, n2 - 1) + a3 * n3 * Math.Pow(b, n3 - 1)); } }

        public static FuncRealFunction f
        {
            get
            {
                return new FuncRealFunction(x =>
                    - (b1 * Math.Pow(x, k1) + b2 * Math.Pow(x, k2) + b3) * (a1 * n1 * (n1 - 1) * Math.Pow(x, n1 - 2) + a2 * n2 * (n2 - 1) * Math.Pow(x, n2 - 2) + a3 * n3 * (n3 - 1) * Math.Pow(x, n3 - 2))
                    - (b1 * k1 * Math.Pow(x, k1 - 1) + b2 * k2 * Math.Pow(x, k2 - 1)) * (a1 * n1 * Math.Pow(x, n1 - 1) + a2 * n2 * Math.Pow(x, n2 - 1) + a3 * n3 * Math.Pow(x, n3 - 1))
                    + (c1 * Math.Pow(x, p1) + c2 * Math.Pow(x, p2) + c3) * (a1 * n1 * Math.Pow(x, n1 - 1) + a2 * n2 * Math.Pow(x, n2 - 1) + a3 * n3 * Math.Pow(x, n3 - 1))
                    + (d1 * Math.Pow(x, q1) + d2 * Math.Pow(x, q2) + d3) * (a1 * Math.Pow(x, n1) + a2 * Math.Pow(x, n2) + a3 * Math.Pow(x, n3) + a4)
                );
            }
        }

        public static BaseRealFunction L(BaseRealFunction func)
        {
            BaseRealFunction functionalDerivative = func.GetNthFunctionalDerivative(1);
            return functionalDerivative.Mult(k).GetNthFunctionalDerivative(1).Minus().Sum(
                p.Mult(functionalDerivative)).Sum(
                q.Mult(func));
        }

        public static BaseRealFunction Solution
        {
            get
            {
                return new FuncRealFunction(x => a1 * Math.Pow(x, n1) + a2 * Math.Pow(x, n2) + a3 * Math.Pow(x, n3) + a4);
            }
        }

    }
}
