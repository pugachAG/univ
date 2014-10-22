using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMMath.Helpers;

namespace WPMMath.Probability.Distributions
{
    /// <summary>
    /// Box–Muller transform based Normal Distribution Generator
    /// </summary>
    public class NormalDistributionGenerator: IRandomGenerator
    {
        private decimal mean;
        private decimal variance;
        private UniformDistributionGenerator standatdUniformDistributionGenerator = new UniformDistributionGenerator();
        
        /// <summary>
        /// Initializes NormalDistributionGenerator instance with mean = 0 and variance = 1 
        /// </summary>
        public NormalDistributionGenerator()
        {
            this.mean = 0;
            this.variance = 1;
        }

        /// <summary>
        /// Initializes NormalDistributionGenerator instance with given mean and variance 
        /// </summary>
        /// <param name="mean">mean</param>
        /// <param name="variance">variance</param>
        public NormalDistributionGenerator(decimal mean, decimal variance)
        {
            Contract.Requires(variance >= 0, "Variance non-negative");
            this.mean = mean;
            this.variance = variance;
        }

        public decimal Mean
        {
            get { return mean; }
            set { mean = value; }
        }

        public decimal Variance
        {
            get { return variance; }
            set { variance = value; }
        }

        public decimal GetNext()
        {
            decimal standardDistributedValue = GetStandardNormalDistributionValue();
            return mean + standardDistributedValue * (decimal) Math.Sqrt((double)variance);
        }

        private decimal GetStandardNormalDistributionValue()
        {
            double u1 = (double)standatdUniformDistributionGenerator.GetNext();
            double u2 = (double)standatdUniformDistributionGenerator.GetNext();
            double result = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
            return (decimal)result;
        }

    }
}
