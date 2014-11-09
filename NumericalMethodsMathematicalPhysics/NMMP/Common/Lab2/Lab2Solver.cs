using Common.Algebra;
using Common.RealAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Lab2
{
    public static class Lab2Solver
    {
        public static UniformGridRealFunction Solve()
        {
            int n = InputData2.PointsCount;
            double h = (InputData2.b - InputData2.a) / n;
            Matrix<double> A = new Matrix<double>(n + 1, n + 1);
            Matrix<double> B = new Matrix<double>(n + 1, 1);
            Func<int, double> a = j => InputData2.k.GetValue(InputData2.a + (j - 0.5) * h);
            Func<int, double> d = j => 0.5 * InputData2.q.GetValue(InputData2.a + (j - 0.5) * h) + 0.5 * InputData2.q.GetValue(InputData2.a + (j + 0.5) * h);
            Func<int, double> phi = j => 0.5 * InputData2.f.GetValue(InputData2.a + (j-0.5) * h) + 0.5 * InputData2.f.GetValue(InputData2.a + (j + 0.5) * h);

            A[0, 0] = a(1) + h * (InputData2.alpha1 + d(0) * h / 2);
            A[0, 1] = - a(1);
            B[0, 0] = h * (InputData2.mu1 + phi(0) * h / 2);

            A[n, n] = a(n) + h * (InputData2.alpha2 + d(n) * h / 2);
            A[n, n - 1] = -a(n);
            B[n, 0] = h * (InputData2.mu2 + phi(n) * h / 2);

            for(int i = 1; i < n; i++)
            {
                A[i, i - 1] = a(i);
                A[i, i] = -(a(i) + a(i + 1) + h * h * d(i));
                A[i, i + 1] = a(i + 1);
                B[i, 0] = h * h * phi(i);
            }
            Matrix<double> res = LinearEquationsSolver.Solve(A, B);
            return new UniformGridRealFunction(res, InputData2.a, InputData2.b, n);
        }
    }
}
