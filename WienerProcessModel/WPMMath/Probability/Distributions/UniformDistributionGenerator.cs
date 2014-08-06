using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMMath.Helpers;

namespace WPMMath.Probability.Distributions
{
    public class UniformDistributionGenerator: IRandomGenerator
    {
        private decimal a;
        private decimal b;
        private Random random = null;

        /// <summary>
        /// Initializes UniformDistributionGenerator instance at interval (0, 1) (standard uniform distribution)
        /// </summary>
        public UniformDistributionGenerator()
        {
            this.a = 0;
            this.b = 1;
            this.random = RandomHelper.CreateRandom();
        }

        /// <summary>
        /// Initializes UniformDistributionGenerator instance at interval (a, b)
        /// </summary>
        /// <param name="a">left bound</param>
        /// <param name="b">right bound</param>
        public UniformDistributionGenerator(decimal a, decimal b)
            : this()
        {
            Contract.Requires(a <= b, "Left bound not greater than right");
            this.a = a;
            this.b = b;
        }

        public decimal GetNext()
        {
            decimal standardDistributedValue = GenerateStandardUniformDistributionValue();
            decimal result = standardDistributedValue * (b - a) + a;
            return result;
        }

        private decimal GenerateStandardUniformDistributionValue()
        {
            decimal result = (decimal)this.random.NextDouble();
            return result;
        }
    }
}
