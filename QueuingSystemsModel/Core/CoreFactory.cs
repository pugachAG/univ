using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystemsModel
{
    public static class CoreFactory
    {
        public static CoreViewModel CreateCoreExponentianInflowConstServingTime(double lambda, double servTime, int servCount)
        {
            return new CoreViewModel(new ExponentialDistribution(lambda), new ConstantDistribution(servTime), servCount);
        }

        public static CoreViewModel CreateCoreExponentianInflowExponentianServTime(double lambda, double servTime, int servCount)
        {
            return new CoreViewModel(new ExponentialDistribution(lambda), new ExponentialDistribution(1.0 / servTime), servCount);
        }
    }
}
