using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethLab
{
    public static class MathHelper
    {
        public const int PointCount = 1000;
        public const double Epsilon = 0.0000001;

        public static double Integrate(Func<double, double> f, double a, double b)
        {
            decimal result = 0;
            double dx = (b - a) / PointCount;
            for (double x = a; x < b; x += dx)
                result += (decimal)f(x + dx/2) * (decimal)dx;
            return (double)result;
        }

        public static double SquareMean(Func<double, double> f, double a, double b, int m)
        {
            decimal result = 0;
            double dx = (b - a) / m;
            for (double x = a; x <= b + dx / 2; x += dx)
                result += (decimal)f(x) / (m + 1);
            return (double)result;
        }

        public static double ScalarProduct(Func<double, double> f, Func<double, double> g, double a, double b)
        {
            Func<double, double> mult = x => f(x) * g(x);
            return Integrate(mult, a, b);
        }

        public static double SquareDifference(Func<double, double> f, Func<double, double> g, double a, double b, int m)
        {
            Func<double, double> mult = x => f(x) * g(x);
            return SquareMean(mult, a, b, m);
        }

        public static double[] SolveSystemOfLinearEquations(double[,] a, double[] b)
        {
            int n = b.Length;
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                int indx = -1;
                for(int j = 0; j < n; j++)
                    if (Math.Abs(a[i, j]) > Epsilon)
                    {
                        indx = j;
                        break;
                    }
                if (indx == -1)
                    return null;

                for (int j = 0; j < n; j++)
                {
                    if (j == i)
                        continue;
                    double k = a[j, indx] / a[i, indx];
                    for (int u = indx; u < n; u++)
                        a[j, u] -= a[i, u] * k;
                    b[j] -= b[i] * k;
                }
            }
            for (int i = 0; i < n; i++)
            {
                double mx = 0;
                for (int j = 0; j < n; j++)
                    if (Math.Abs(a[i, j]) > Math.Abs(mx))
                        mx = a[i, j];
                x[i] = b[i] / mx;
                
            }
            return x;
        }

        public static Matrix SolveSystemOfLinearEquations(Matrix A, Matrix B)
        {
            double[,] a = new Matrix(A).Data;
            double[] b = new double[B.RowCount];
            for (int i = 0; i < b.Length; i++)
                b[i] = B[i,0];
            double[] x = SolveSystemOfLinearEquations(a, b);
            Matrix res = new Matrix(x.Length, 1);
            for (int i = 0; i < x.Length; i++)
                res[i, 0] = x[i];
            return res;
        }
    }
}
