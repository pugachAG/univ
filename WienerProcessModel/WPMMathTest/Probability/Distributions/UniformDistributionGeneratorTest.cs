using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPMMath.Probability.Distributions;
using WPMMathTest.Helpers;
using System.Linq;

namespace WPMMathTest
{
    [TestClass]
    public class UniformDistributionGeneratorTest
    {
        const decimal a = -100.0M;
        const decimal b =  1000.0M;
        const int CheckIntervalsCount = 10;
        const decimal DistributionDeltaPassRatio = 0.1M;

        private decimal Mean
        {
            get
            {
                return (a + b) / 2;
            }
        }

        private decimal Variance
        {
            get
            {
                decimal diff = b - a;
                decimal k = 1.0M / 12.0M;
                return k * diff * diff;
            }
        }

        [TestMethod]
        public void UniformDistributionMeanTest()
        {
            UniformDistributionGenerator instance = new UniformDistributionGenerator(a, b);
            decimal meanGenerated = DistributionsTestHelper.GenerateMeanValue(instance);
            AssertHelper.AreEqual(Mean, meanGenerated, DistributionsTestHelper.GetMeanDelta(Variance));
        }

        [TestMethod]
        public void UniformDistributionMainTest()
        {
            UniformDistributionGenerator instance = new UniformDistributionGenerator(a, b);
            int[] counts = new int[CheckIntervalsCount];
            for (int i = 0; i < DistributionsTestHelper.GeneratedValuesCount; i++)
            {
                decimal value = instance.GetNext();
                int index = GetIntervalIndex(value);
                counts[index]++;
            }
            int deltaCount = counts.Max() - counts.Min();

            Assert.IsTrue(deltaCount < DistributionDeltaPassRatio * DistributionsTestHelper.GeneratedValuesCount);
        }

        public int GetIntervalIndex(decimal value)
        {
            decimal shiftedValue = value - a;
            decimal ratio = shiftedValue / (b - a);
            int index = (int)(CheckIntervalsCount * ratio);
            return index;
        }

    }
}
