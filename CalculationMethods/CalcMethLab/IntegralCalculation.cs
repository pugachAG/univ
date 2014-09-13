using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethLab
{
    public class IntegralCalculation
    {
        public const double Epsilon = 0.001;

        public IntegralCalculation()
        {
            this.F = x => Math.Exp(-2 * x) * Math.Sin(2 * x);
        }

        public string IterationProcess { get; private set; }


        public Func<double, double> F { get; set; }

        public double GetUpperBound()
        {
            // Math.Exp(-2 * x)  is upper bound of F
            // integrate(Math.Exp(-2 * x)) from a to inf is 0.5 * Math.Exp(-2 * a)
            // 0.5 * Math.Exp(-2 * a) < Epsilon / 2
            // -2 * a < Math.Log(Epsilon)
            // a > - 0.5 * Math.Log(Epsilon)
            return - 0.5 * Math.Log(Epsilon);
        }

        private double CalcSimpson(double a, double b, int n)
        {
            double dx = (b - a) / n;
            double result = 0;
            for (int i = 0; i <= n; i++)
            {
                double x = a + i * dx;
                double k = (i % n == 0) ? 1 : (i % 2 == 1 ? 4 : 2);
                result += this.F(x) * k;
            }
            result *= dx / 3;
            return result;
        }

        private double GetSimpsonCoef()
        {
            return 1.0 / 15.0;
        }

        public double getPreciseValue()
        {
            return 0.25;
        }

        public double Integrate()
        {
            StringBuilder iterationProcessBuilder = new StringBuilder();

            double a = 0, b = GetUpperBound();
            int n = 2;
            double prev = CalcSimpson(a, b, n);
            while (true)
            {
                iterationProcessBuilder.Append(string.Format("I(h/{0}) = {1}", n, prev) + Environment.NewLine);
                n *= 2;
                double cur = CalcSimpson(a, b, n);

                if (Math.Abs((cur - prev) * GetSimpsonCoef()) < Epsilon / 2)
                {
                    this.IterationProcess = iterationProcessBuilder.ToString();
                    return cur;
                }
                prev = cur;
            }
        }
    }
}
