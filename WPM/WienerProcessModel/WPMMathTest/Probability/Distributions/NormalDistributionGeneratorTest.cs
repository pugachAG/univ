using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPMMath.Probability.Distributions;
using WPMMathTest.Helpers;
using System.Collections.Generic;

namespace WPMMathTest
{
    [TestClass]
    public class NormalDistributionGeneratorTest
    {
        readonly decimal[] MeanValues = { -10, 0, 100 };
        readonly decimal[] VarianceValues = { 1, 1, 1000 };
        const decimal DistributionDeltaPassRatio = 0.1M;

        [TestMethod]
        public void NormalDistributionMeanTest()
        {
            for (int i = 0; i < MeanValues.Length; i++)
            {
                NormalDistributionGenerator instance = new NormalDistributionGenerator(MeanValues[i], VarianceValues[i]);
                decimal meanGenerated = DistributionsTestHelper.GenerateMeanValue(instance);
                AssertHelper.AreEqual(MeanValues[i], meanGenerated, DistributionsTestHelper.GetMeanDelta(VarianceValues[i]));
            }
        }

        /// <summary>
        /// Test using fact, that
        /// About 68% of values drawn from a normal distribution are within one standard deviation σ away from the mean
        /// </summary>
        [TestMethod]
        public void NormalDistributionMainTest()
        {
            List<Tuple<decimal, decimal>> testcases = new List<Tuple<decimal, decimal>>
            {
                Tuple.Create(1.0M, 0.682689492137M),
                Tuple.Create(2.0M, 0.954499736104M),
                Tuple.Create(1.281551565545M, 0.8M)
            };

            for (int i = 0; i < MeanValues.Length; i++)
            {
                foreach (var pair in testcases)
                    TestTolerance(MeanValues[i], VarianceValues[i], pair.Item2, pair.Item1);

            }
        }

        private void TestTolerance(decimal mean, decimal variance, decimal ratioInside, decimal n)
        {
            NormalDistributionGenerator instance = new NormalDistributionGenerator(mean, variance);
            decimal varianceSqrt = (decimal)Math.Sqrt((double)variance);
            int countInside = 0;
            for(int i = 0; i < DistributionsTestHelper.GeneratedValuesCount; i++)
            {
                decimal value = instance.GetNext();
                if (mean - n * varianceSqrt < value && value < mean + n * varianceSqrt)
                    countInside++;
            }
            decimal ratio = (decimal)countInside / DistributionsTestHelper.GeneratedValuesCount;
            AssertHelper.AreEqual(ratioInside, ratio, DistributionDeltaPassRatio);
        }

    }
}
