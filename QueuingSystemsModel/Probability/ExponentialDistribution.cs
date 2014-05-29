using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystemsModel.Probability
{
    public class ExponentialDistribution : IDistribution
    {
        private const double maxValue = 100;
        private const int intervalsCount = 10000;

        public double Lambda { get; private set; }

        public ExponentialDistribution(double lambda)
        {
            this.Lambda = lambda;
        }

        public double DistributionFunctionValue(double x)
        {
            if (x < 0)
                return 0;
            return 1.0 - Math.Exp(-this.Lambda * x);
        }

        public double GetDistributionRandomValue()
        {
            double x = RandomNumberGenerator.GetRandomDouble();
            return x;
        }
    }

}
