using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Algebra
{
    public static class LinearEquationsSolver
    {
        public const decimal Epsilon = 0.00001M;

        public static Matrix<T> Solve<T>(Matrix<T> a, Matrix<T> b)
        {
            if (a.RowsCount != a.ColumnsCount || b.RowsCount != a.RowsCount || b.ColumnsCount != 1)
                throw new ArgumentException();
            Matrix<decimal> x = Solve(ConvertMatrix<T, decimal>(a), ConvertMatrix<T, decimal>(b));
            return ConvertMatrix<decimal, T>(x);               
        }

        private static Matrix<decimal> Solve(Matrix<decimal> a, Matrix<decimal> b)
        {
            int n = a.RowsCount;
            HashSet<int> usedRows = new HashSet<int>();
            for(int i = 0; i < n; i++)
            {
                int currentRowRumber = -1;
                for (int row = 0; row < n; row++)
                {
                    if(!usedRows.Contains(row) && Math.Abs(a[row, i]) > Epsilon)
                    {
                        currentRowRumber = row;
                        break;
                    }
                }
                usedRows.Add(currentRowRumber);
                if(currentRowRumber == -1)
                    return null;

                for (int row = 0; row < n; row++)
                    if(row != currentRowRumber)
                    {
                        decimal k = a[row, i] / a[currentRowRumber, i];
                        for (int j = 0; j < n; j++)
                            a[row, j] -= k * a[currentRowRumber, j];
                        b[row, 0] -= k * b[currentRowRumber, 0];
                    }
            }
            Matrix<decimal> result = new Matrix<decimal>(n, 1);
            for (int i = 0; i < n; i++)
            {
                decimal maxValue = 0;
                int maxIndex = -1;
                for (int j = 0; j < n; j++)
                {
                    decimal val = Math.Abs(a[i, j]);
                    if(val > maxValue)
                    {
                        maxValue = val;
                        maxIndex = j;
                    }
                }
                result[i, 0] = b[maxIndex, 0] / a[i, maxIndex];
            }
            return result;
        }


        private static Matrix<Tout> ConvertMatrix<Tin, Tout>(Matrix<Tin> matrix)
        {
            if (matrix == null)
                return null;
            Matrix<Tout> result = new Matrix<Tout>(matrix.RowsCount, matrix.ColumnsCount);
            for(int i = 0; i < matrix.RowsCount; i++)
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    result[i, j] = (Tout)Convert.ChangeType(matrix[i, j], typeof(Tout));
                }
            return result;
        }
    }
}
