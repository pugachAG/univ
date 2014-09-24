using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Algebra;

namespace Tests.Algebra
{
    [TestClass]
    public class LinearEquationsSolverTester
    {
        [TestMethod]
        public void RandomTestLinearEquationsSolver()
        {
            int n = 100;
            Matrix<double> a = new Matrix<double>(n, n);
            Matrix<double> b = new Matrix<double>(n, 1);
            Random rand = new Random(42);
            Func<double> getNextVal = () => 100 * rand.NextDouble();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = getNextVal();
                }
                b[i, 0] = getNextVal();
            }
            Matrix<double> x = LinearEquationsSolver.Solve(a, b);
            Matrix<double> actualB = a * x;
            double delta = (double)LinearEquationsSolver.Epsilon;
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(actualB[i, 0], b[i, 0], delta, string.Format("expectedB[{0},0] != b[{0},0]", i));
            }
        }

        [TestMethod]
        public void SmallTestLinearEquationsSolver()
        {
            Matrix<double> a = new Matrix<double>(2, 2);
            a[0, 0] = 1;
            a[0, 1] = 2;
            a[1, 0] = 3;
            a[1, 1] = 4;
            Matrix<double> b = new Matrix<double>(2, 1);
            b[0, 0] = 17;
            b[1, 0] = 39;
            Matrix<double> x = LinearEquationsSolver.Solve<double>(a, b);
            Matrix<double> actualB = a * x;
            double delta = (double)LinearEquationsSolver.Epsilon;
            Assert.AreEqual(actualB[0, 0], b[0, 0], delta, "expectedB[0,0] != b[0,0]");
            Assert.AreEqual(actualB[1, 0], b[1, 0], delta, "expectedB[1,0] != b[1,0]");
        }
    }
}
