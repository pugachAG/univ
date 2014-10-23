using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPMMath.Helpers
{
    public static class RandomHelper
    {
        private static Random random;

        static RandomHelper()
        {
            random = new Random();
        }

        /// <summary>
        /// Creates random instance with random seed
        /// </summary>
        /// <returns>Random instance</returns>
        public static Random CreateRandom()
        {
            int seed = random.Next();
            Random result = new Random(seed);
            return result;
        }

    }
}
