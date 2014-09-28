using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Lab1;
using Common.RealAnalysis;

namespace Tests.Lab1
{
    [TestClass]
    public class Lab1Tester
    {
        const double DeltaMulter = 0.01;
        const double MinDelta = 0.5;
        private static BaseRealFunction solution = null;

        static Lab1Tester()
        {
            solution = BubnovGalerkinMethod.GetSolution();
        }

        [TestMethod]
        public void TestInputBubnovGalerkinSolution()
        {
            BaseRealFunction actualF = InputData.L(InputData.Solution);
            BaseRealFunction expectedF = InputData.f;

            double dx = (InputData.b - InputData.a) / 100;
            for (double x = InputData.a + dx; x < InputData.b; x += dx)
            {
                double delta = Math.Max(Math.Abs(expectedF.GetValue(x)) * DeltaMulter, MinDelta);
                Assert.AreEqual(expectedF.GetValue(x), actualF.GetValue(x), delta);
            }
        }

        [TestMethod]
        public void TestBubnovGalerkinMethod()
        {
            BaseRealFunction expectedF = InputData.Solution;

            double dx = (InputData.b - InputData.a) / 100;
            for (double x = InputData.a + dx; x < InputData.b; x += dx)
            {
                double delta = Math.Max(Math.Abs(expectedF.GetValue(x)) * DeltaMulter, MinDelta);
                Assert.AreEqual(expectedF.GetValue(x), solution.GetValue(x), delta);
            }
        }

        [TestMethod]
        public void TestBubnovGalerkinMethodInitialRestrictions()
        {
            BaseRealFunction expected = InputData.Solution;
            CheckInitialRestictions(expected);
            CheckInitialRestictions(solution);
        }

        private void CheckInitialRestictions(BaseRealFunction function)
        {
            double v1 = InputData.alpha * function.GetNthFunctionalDerivative(1).GetValue(InputData.a) -
               InputData.beta * function.GetValue(InputData.a);

            double v2 = InputData.gamma * function.GetNthFunctionalDerivative(1).GetValue(InputData.b) -
               InputData.delta * function.GetValue(InputData.b);

            Assert.AreEqual(0, v1, MinDelta);
        }

    }
}
