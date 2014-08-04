using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMMath.Helpers;

namespace WPMMath.Probability.Distributions
{
    public class NormalDistributionGenerator: IRandomGenerator
    {
        private decimal mean;
        private decimal variance;
        private Random random = null;
        
        /// <summary>
        /// Initializes NormalDistributionGenerator instance with mean = 0 and variance = 1 
        /// </summary>
        public NormalDistributionGenerator()
        {
            this.mean = 0;
            this.variance = 1;
            this.random = RandomHelper.CreateRandom();
        }

        /// <summary>
        /// Initializes NormalDistributionGenerator instance with given mean and variance 
        /// </summary>
        /// <param name="mean">mean</param>
        /// <param name="variance">variance</param>
        public NormalDistributionGenerator(decimal mean, decimal variance)
            : this()
        {
            this.mean = mean;
            this.variance = variance;
        }

        public decimal GetNext()
        {
            throw new NotImplementedException();
        }
    }
}
