using Common.Algebra;
using Common.RealAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Lab1
{
    public static class BubnovGalerkinMethod
    {
        private static HilbertSpace scalarProductSpace = new HilbertSpace(InputData.a, InputData.b);
        private static BaseRealFunction[] phi = new BaseRealFunction[InputData.BasisFunctionsCount];
        private static BaseRealFunction[] Aphi = new BaseRealFunction[InputData.BasisFunctionsCount];


        static BubnovGalerkinMethod()
        {
            InitPhi();
        }

        public static BaseRealFunction GetPhi(int i)
        {
            return phi[i];
        }

        public static FuncRealFunction GetSolution()
        {
            int n = InputData.BasisFunctionsCount;
            Matrix<double> a = new Matrix<double>(n, n);
            Matrix<double> b = new Matrix<double>(n, 1);
            for (int row = 0; row < n; row++)
            {
                for (int column = 0; column < n; column++)
                {
                    a[row, column] = scalarProductSpace.GetScalarProduct(Aphi[column], phi[row]);
                }
                b[row, 0] = scalarProductSpace.GetScalarProduct(InputData.f, phi[row]);
            }
            Matrix<double> c = LinearEquationsSolver.Solve(a, b);

            FuncRealFunction result = new FuncRealFunction(x =>
                {
                    double res = 0;
                    for (int i = 0; i < InputData.BasisFunctionsCount; i++)
                    {
                        res += c[i, 0] * phi[i].GetValue(x);
                    }
                    return res;
                });
            return result;
        }

        public static async Task<FuncRealFunction> GetSolutionAsync()
        {
            var task = new Task<FuncRealFunction>(GetSolution);
            task.Start();
            FuncRealFunction solution = await task;
            return solution;
        }

        private static void InitPhi()
        {
            phi[0] = new FuncRealFunction(x => Math.Pow(x - InputData.a, 2) * (x - A));
            phi[1] = new FuncRealFunction(x => Math.Pow(x - InputData.b, 2) * (x - B));
            for (int i = 2; i < InputData.BasisFunctionsCount; i++)
            {
                int j = i;
                phi[j] = new FuncRealFunction(x => Math.Pow(x - InputData.a, j) * Math.Pow(x - InputData.b, 2));
            }

            for (int i = 0; i < InputData.BasisFunctionsCount; i++)
            {
                Aphi[i] = InputData.L(phi[i]);
            }
        }

        public static double A
        {
            get
            {
                return InputData.b - InputData.gamma * (InputData.a - InputData.b) / (2 * InputData.gamma + InputData.delta * (InputData.b - InputData.a));
            }
        }

        public static double B
        {
            get
            {
                return InputData.a - InputData.alpha * (InputData.b - InputData.a) / (2 * InputData.alpha - InputData.beta * (InputData.a - InputData.b));
            }
        }

    }
}
