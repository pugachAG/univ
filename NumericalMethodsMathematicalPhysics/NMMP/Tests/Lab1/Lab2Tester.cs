using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Lab2;
using Common.RealAnalysis;

namespace Tests.Lab1
{
    [TestClass]
    public class Lab2Tester
    {
        [TestMethod]
        public void SolutionMatchingWithSolution()
        {
            var sol = InputData2.Solution;
            ValidateSolution(sol);
        }

        [TestMethod]
        public void SolutionMatchingWithBoundaryConditions()
        {
            var sol = InputData2.Solution;
            double v1 = getVal(InputData2.alpha1, InputData2.mu1, InputData2.a, -1);
            double v2 = getVal(InputData2.alpha2, InputData2.mu2, InputData2.b, 1);
            Assert.AreEqual(v1, 0, 0.1);
            Assert.AreEqual(v2, 0, 0.1);
        }

        [TestMethod]
        public void Lab2SolverWithSolution()
        {
            var sol = Lab2Solver.Solve();
            ValidateSolution(sol);
        }

        private void ValidateSolution(BaseRealFunction sol)
        {
            var diff = InputData2.Solution.Sum(sol.Minus()); // InputData2.L(sol).Sum(InputData2.f.Minus());
            for (double i = InputData2.a; i <= InputData2.b; i += 0.1)
            {
                Assert.AreEqual(0, diff.GetValue(i), 0.1);
            }
        }

        private double getVal(double alpha, double mu, double x, int coef)
        {
            var sol = InputData2.Solution;
            return coef * sol.GetNthFunctionalDerivative(1).GetValue(x) * InputData2.k.GetValue(x) + sol.GetValue(x) * alpha - mu;
                 
        }
    }
}
