using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystemsModel.Probability
{
    public static class RandomNumberGenerator
    {
        private static Random rand = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static double GetRandomDouble()
        {
            return rand.NextDouble();
        }
    }
}
