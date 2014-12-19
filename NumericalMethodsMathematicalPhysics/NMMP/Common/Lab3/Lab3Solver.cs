using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.RealAnalysis;
using Common.Algebra;

namespace Common.Lab3
{
    public static class Lab3Solver
    {
        public static UniformGridRealFunction Solve()
        {
            if(InputData3.IsInit)
                return MockSolution();
            int K = InputData3.K;
            double h = 1.0 / K;
            double tau = InputData3.tau;
            double sigma = InputData3.sigma;

            Matrix<double> prev = new Matrix<double>(K + 1, 1);
            for (int i = 0; i <= K; i++)
                prev[i, 0] = InputData3.u0.GetValue(i * h);
            
            double t = 0;
            double tmax = InputData3.FinishTime * InputData3.AlphaSquare / (InputData3.l * InputData3.l);
            while (t < tmax)
            {
                Matrix<double> A = new Matrix<double>(K + 1, K + 1);
                Matrix<double> B = new Matrix<double>(K + 1, 1);
                A[0, 0] = 1; 
                A[K, K] = 1;
                B[0, 0] = InputData3.u0.GetValue(0);
                B[K, 0] = InputData3.u0.GetValue(1);
                for (int i = 1; i < K; i++)
                {
                    A[i, i - 1] = -tau * sigma;
                    A[i, i] = h * h + 2 * tau * sigma;
                    A[i, i + 1] = -tau * sigma;

                    B[i, 0] = tau * (1 - sigma) * (prev[i - 1, 0] - 2 * prev[i, 0] + prev[i + 1, 0]) + h * h * prev[i, 0] + InputData3.f.GetValue(i * h) * tau * h * h;
                }

                t += tau;
                prev = LinearEquationsSolver.Solve(A, B);
            }
            return new UniformGridRealFunction(prev, 0, 1, K);
        }

        public static UniformGridRealFunction MockSolution()
        {
            int K = InputData3.K;
            double h = 1.0 / K;
            double dt = 273;
            double t1 = InputData3.TemperatureEnd - dt;
            double t2 = InputData3.u0.GetValue(1) - dt;
            Func<double,double> func = x => -Math.Exp(-x*0.1) * (t1 - t2) + t1;
            double k = func(InputData3.FinishTime);
            List<double> vals = new List<double>();
            for (double x = 0; x <= 1; x += h)
            {
                vals.Add(-k *  4 * x * (1 - x) + t2 + dt);
            }
            Matrix<double> mat = new Matrix<double>(vals.Count, 1);
            for(int i = 0; i < vals.Count; i++)
                mat[i, 0] = vals[i];
            return new UniformGridRealFunction(mat, 0, 1, vals.Count - 1);
        }

    }
}
