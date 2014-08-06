using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMMath.Probability.Distributions;

namespace WPMMathTest.Helpers
{
    public static class DistributionsTestHelper
    {
        public const int GeneratedValuesCount = 1000000;
        public const decimal MeanDeltaPassFactor = 100.0M;

        public static decimal GenerateMeanValue(IRandomGenerator generator)
        {
            decimal sum = 0;
            for (int i = 0; i < DistributionsTestHelper.GeneratedValuesCount; i++)
            {
                sum += generator.GetNext();
            }
            decimal meanGenerated = sum / DistributionsTestHelper.GeneratedValuesCount;
            return meanGenerated;
        }

        /// <summary>
        /// According to central limit theorem
        /// </summary>
        /// <param name="variance">variance value</param>
        /// <returns></returns>
        public static decimal GetMeanDelta(decimal variance)
        {
            decimal varianceSqrt = (decimal)Math.Sqrt((double)variance);
            return varianceSqrt * MeanDeltaPassFactor / (decimal)Math.Sqrt(GeneratedValuesCount);
        }
    }
}
