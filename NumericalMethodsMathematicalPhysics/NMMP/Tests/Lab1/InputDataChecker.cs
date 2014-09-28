using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Lab1;
using Common.RealAnalysis;

namespace Tests.Lab1
{
    [TestClass]
    public class InputDataChecker
    {
        const double DeltaMulter = 0.01;
        const double MinDelta = 0.5;

        [TestMethod]
        public void TestSolution()
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
    }
}
