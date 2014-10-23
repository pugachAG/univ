using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMMath.Common;
using WPMMath.Probability.Distributions;

namespace WPMMath.Probability
{
    public class WienerProcess
    {
        private NormalDistributionGenerator randomGenerator = new NormalDistributionGenerator();

        public DiscreteFunction Generate(decimal[] args)
        {
            decimal[] timePoints = args.ToArray();
            Array.Sort<decimal>(args);
            DiscreteFunction result = new DiscreteFunction();
            decimal prevArg = timePoints[0];
            decimal prevVal = 0;
            result.AddValue(prevArg, prevArg);
            for (int i = 1; i < timePoints.Length; i++)
            {
                decimal dt = timePoints[i] - prevArg;
                randomGenerator.Variance = dt;
                prevVal += randomGenerator.GetNext();
                prevArg = timePoints[i];
                result.AddValue(prevArg, prevVal);
            }
            return result;
        }

        public Task<DiscreteFunction> GenerateAsync(decimal[] args)
        {
            return Task<DiscreteFunction>.Run(() => Generate(args));
        }
            
    }
}
