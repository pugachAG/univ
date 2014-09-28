using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.RealAnalysis;

namespace Tests.RealAnalysis
{
    [TestClass]
    public class FunctionalDerivativeTester
    {
        public const double Delta = 0.5;

        Random rand = new Random(42);

        [TestMethod]
        public void TestFunctionalDerivative()
        {
            CheckValue(new FuncRealFunction(x => x).GetNthFunctionalDerivative(1), 1, 1);
            CheckValue(new FuncRealFunction(x => x * x).GetNthFunctionalDerivative(1), 1, 2);
            CheckValue(new FuncRealFunction(x => x * x).GetNthFunctionalDerivative(1).GetNthFunctionalDerivative(1), 1, 2);
            CheckValue(new FuncRealFunction(x => Math.Exp(x)).
                GetNthFunctionalDerivative(4), 
                1, Math.Exp(1));


            Func<double, double> func = x => 31 * x * x * x + 54 * x * x + 75 * x + 228;
            Func<double, double> actual1stDerived = x => 93 * x * x + 108 * x + 75;
            Func<double, double> actual2ndDerived = x => 186 * x + 108;
            Func<double, double> actual3rdDerived = x => 186;
            Func<double, double>[] deriveds = new Func<double, double>[]
            {
                actual1stDerived,
                actual2ndDerived,
                actual3rdDerived
            };

            int testCasesCount = 100;
            FuncRealFunction function = new FuncRealFunction(func);
            for (int i = 0; i < testCasesCount; i++)
            {
                double arg = 1 * rand.NextDouble();
                for (int n = 1; n <= 3; n++)
                {
                    CheckValue(function.GetNthFunctionalDerivative(n), arg, deriveds[n - 1](arg));
                }
            }

            
        }

        private void CheckValue(BaseRealFunction function, double arg, double preciseValue)
        {
            double actualValue = function.GetValue(arg);
            Assert.AreEqual(preciseValue, actualValue, Delta);
        }
    }
}
