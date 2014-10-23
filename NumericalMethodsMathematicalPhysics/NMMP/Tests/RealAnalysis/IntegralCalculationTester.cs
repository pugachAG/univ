using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.RealAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.RealAnalysis
{
    [TestClass]
    public class IntegralCalculationTester
    {
        const double Delta = 0.1;

        [TestMethod]
        public void TestIntegralCalculation()
        {
            CheckIntegralValue(new Func<double, double>(x => 2).ToRealFunction(), 0, 2, 4);
            CheckIntegralValue(new Func<double, double>(x => x).ToRealFunction(), 0, 2, 2);
            CheckIntegralValue(new Func<double, double>(x => x * x).ToRealFunction(), 0, 2, 8.0 / 3.0);
            CheckIntegralValue(new Func<double, double>(x => Math.Sin(x)).ToRealFunction(), 0, 2 * Math.PI, 0);
            CheckIntegralValue(new Func<double, double>(x => Math.Sin(x)).ToRealFunction(), 0, 100 * Math.PI, 0);
            CheckIntegralValue(new Func<double, double>(x => Math.Exp(x)).ToRealFunction(), 0, 4, Math.Exp(4) - 1.0);
        }

        private void CheckIntegralValue(BaseRealFunction function, double a, double b, double preciseValue)
        {
            double actualValue = IntegralCalculator.RiemannIntegral(function, a, b);
            Assert.AreEqual(preciseValue, actualValue, Delta);
        }
    }
}
