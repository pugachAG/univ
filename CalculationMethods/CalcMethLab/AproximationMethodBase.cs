using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethLab
{
    public abstract class AproximationMethodBase
    {
        public Func<double, double> f;
        public double a;
        public double b;
        public double diff;
        protected int n;
        protected int m;
        protected Func<double, double>[] phi;
        protected bool isTrig = false;
        protected bool isDiscrete = false;
        

        protected AproximationMethodBase()
        {
            double k = 0;
            double c = 2;
            double w = 7;
            this.f = x => k * Math.Log(x) + c * Math.Cos(w * x);
            //this.f = x => x * Math.Sqrt(x);
            this.n = 3;
            this.initTrig();
            //this.initExp();
            this.a = 1;
            this.b = 3;
            this.m = 20;
        }

        public int M
        {
            get
            {
                return this.m;
            }
        }

        public virtual Func<double, double> Solve()
        {
            if (isTrig)
                return SolveTrig();

            double[,] a = new double[n + 1, n + 1];
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= n; j++)
                    a[i, j] = this.CalcScalarProduct(phi[j], phi[i]);
            double[] b = new double[n+1];
            for (int i = 0; i <= n; i++)
                b[i] = this.CalcScalarProduct(f, phi[i]);
            double[] c = MathHelper.SolveSystemOfLinearEquations(a, b);
            Func<double, double> g = x =>
                {
                    double res = 0;
                    for (int i = 0; i <= n; i++)
                        res += c[i] * phi[i](x);
                    return res;
                };
            Func<double, double> dfg = x => f(x) - g(x);
            this.diff = this.CalcScalarProduct(dfg, dfg);
            return g;
        }

        public Func<double, double> SolveTrig()
        {
            Func<double, double>[] sin = new Func<double, double>[n + 1];
            Func<double, double>[] cos = new Func<double, double>[n + 1];
            for (int i = 1; i <= n; i++)
            {
                int j = i;
                sin[j] = x => Math.Sin(j * x);
                cos[j] = x => Math.Cos(j * x);
            }
            double[] aa = new double[n + 1];
            double[] bb = new double[n + 1];
            Func<double, double> ff = t => f(t * (b - a) / (2 * Math.PI) + a);
            Func<double, double> xTot = x => (2* Math.PI) * (x - a) / (b - a);

            double from = 0;
            double to = 2 * Math.PI;

            aa[0] = (isDiscrete ? 1 : (1 / (2 * Math.PI))) * CalcScalarProduct(ff, x => 1, from , to);
            double coef = this.isDiscrete ? 2  : (1 / Math.PI);
            for (int i = 1; i <= n; i++)
            {
                aa[i] = coef * CalcScalarProduct(ff, cos[i], from, to);
                bb[i] = coef * CalcScalarProduct(ff, sin[i], from, to);
            }
            Func<double, double> g = x =>
                {
                    double res = aa[0];
                    for (int i = 1; i <= n; i++)
                    {
                        res += aa[i] * cos[i](xTot(x));
                        res += bb[i] * sin[i](xTot(x));
                    }
                    return res;
                };
            Func<double, double> dfg = x => f(x) - g(x);
            this.diff = this.CalcScalarProduct(dfg, dfg);
            return g;
        }

        protected abstract double CalcScalarProduct(Func<double, double> f, Func<double, double> g);

        protected abstract double CalcScalarProduct(Func<double, double> f, Func<double, double> g, double from, double to);

        private void initExp()
        {
            phi = new Func<double, double>[n+1];
            for (int i = 0; i <= n; i++)
            {
                int j = i;
                phi[i] = x => Math.Exp(j * x);
            }
            this.isTrig = false;
        }

        private void initTrig()
        {
            phi = new Func<double, double>[n + 1];
            for (int i = 0; i <= n; i++)
            {
                int j = i;
                if(j % 2 == 0)
                    phi[i] = x => Math.Cos(j * x);
                else
                    phi[i] = x => Math.Sin(j * x);
            }
            this.isTrig = true;
        }

        private void initPolin()
        {
            phi = new Func<double, double>[n + 1];
            for (int i = 0; i <= n; i++)
            {
                int j = i;
                phi[i] = x => Math.Pow(x, j);
            }
            this.isTrig = false;
        }
    }
}
