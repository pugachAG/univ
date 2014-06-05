using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystemsModel
{
    public class ConstantDistribution : IDistribution
    {
        private double value;

        public ConstantDistribution(double value)
        {
            this.value = value;
        }

        public double GetDistributionRandomValue()
        {
            return value;
        }

        public double DistributionFunctionValue(double x)
        {
            if (x < value)
                return 0;
            else
                return 1;
        }


        public double Mean
        {
            get { return value; }
        }
    }
}
