using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystemsModel.Probability
{
    public interface IDistribution
    {
        double GetDistributionRandomValue();

        double DistributionFunctionValue(double x);
    }
}
