using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystemsModel
{
    public class ExponentialDistribution : IDistribution
    {
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
            return -1.0 * Math.Log(1 - x) / Lambda; 
        }
    }

}
