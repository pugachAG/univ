using Common.RealAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Lab3
{
    public static class InputData3
    {
        public static double sigma = 0.5;
        public static int K = 100;
        public static double tau = 0.01;

        public static double l = 0.02;
        public static double FinishTime = 10;
        public static double TemperatureEnd = 0;
        public static FuncRealFunction u0 = new FuncRealFunction(
            x => 0
            //x => - 10 * x * Math.Sin(Math.PI * x)
            );
        public static FuncRealFunction f = new FuncRealFunction(
            x => 3500
            );

        public static double lambda = 45.5;
        public static double c = 0.46 * 1000;
        public static double rho = 7900;
        public static double gamma = 140;

        public static double AlphaSquare
        {
            get
            {
                return lambda / (c * rho);
            }
        }
    }
}
