using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethLab
{
    public class SmoothingSpline: AproximationMethodBase
    {
        private const double rho = 10; 

        private double getX(int i)
        {
            double dx = (base.b - base.a) / base.m;
            return base.a + dx * i;
        }

        private double getH(int i)
        {
            return getX(i) - getX(i - 1);
        }

        private Matrix A
        {
            get
            {
                Matrix res = new Matrix(base.m - 1, base.m - 1);
                for (int i = 0; i < m - 1; i++)
                {
                    res[i,i] = (getH(i+1) + getH(i+2))/3;
                    if (i > 0)
                        res[i, i - 1] = getH(i + 1) / 6;
                    if (i < m - 2)
                        res[i, i + 1] = getH(i + 2) / 6;
                }
                return res;
            }
        }

        private Matrix H
        {
            get
            {
                Matrix res = new Matrix(base.m - 1, base.m + 1);
                for (int i = 0; i < m - 1; i++)
                {
                    res[i, i] = 1 / getH(i + 1);
                    res[i, i + 1] = -(1 / getH(i + 1) + 1 / getH(i + 2));
                    res[i, i + 2] = 1 / getH(i + 2);
                }
                return res;
            }
        }

        private Matrix ff
        {
            get
            {
                Matrix res = new Matrix(base.m + 1, 1);
                for (int i = 0; i < m + 1; i++)
                    res[i, 0] = base.f(getX(i));
                return res;
            }
        }
       
        private Matrix Rinverse
        {
            get
            {
                Matrix res = new Matrix(base.m + 1, base.m + 1);
                for (int i = 0; i < m - 1; i++)
                    res[i, i] = 1/rho;
                return res;
            }
        }

        public override Func<double, double> Solve()
        {
            Matrix Left = this.A + (this.H * this.Rinverse) * this.H.Transpose();
            Matrix Right = this.H * this.ff;
            Matrix vecM = MathHelper.SolveSystemOfLinearEquations(Left, Right);

            Matrix ss = Left * vecM - Right;
            for (int i = 0; i < ss.Data.GetLength(0); i++)
                for (int j = 0; j < ss.Data.GetLength(1); j++)
                    if (Math.Abs(ss[i, j]) > MathHelper.Epsilon)
                        throw new Exception();

            Matrix vecMu = ff - this.Rinverse * H.Transpose() * vecM;
            ss = vecMu - ff;
            Matrix tmpM = new Matrix(vecM.RowCount + 2, vecM.ColumnCount);

            for (int i = 1; i < tmpM.RowCount - 1; i++)
                tmpM[i,0] = vecM[i - 1,0];
            vecM = tmpM;

            Func<double, double> g = (x) =>
                {
                    int indx = 0;
                    if (x >= a && x <= b)
                        while (x > getX(indx + 1))
                            indx++;
                    else
                        if (x >= b)
                            indx = m-1;
                    double h = getH(indx + 1);
                    return vecM[indx, 0] * Math.Pow((getX(indx+1) - x), 3) / (6 * h) +
                         vecM[indx+1, 0] * Math.Pow((x-getX(indx)), 3) / (6 * h) +
                         (vecMu[indx, 0] - vecM[indx, 0] * h * h / 6)* (getX(indx+1) - x)/h +
                         (vecMu[indx + 1, 0] - vecM[indx + 1, 0] * h * h / 6) * (x - getX(indx)) / h;

                };

            return g;
        }

        protected override double CalcScalarProduct(Func<double, double> f, Func<double, double> g)
        {
            throw new NotImplementedException();
        }

        protected override double CalcScalarProduct(Func<double, double> f, Func<double, double> g, double from, double to)
        {
            throw new NotImplementedException();
        }
    }
}
