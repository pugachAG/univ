using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Algebra
{
    public class Matrix<T>
    {
        private T[,] data = null;

        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }

        public Matrix(int rows, int columns)
        {
            this.RowsCount = rows;
            this.ColumnsCount = columns;
            this.data = new T[RowsCount, ColumnsCount];
        }

        public T this[int row, int column]
        {
            get
            {
                return data[row, column];
            }
            set
            {
                data[row, column] = value;
            }
        }

        public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
        {
            if (a.ColumnsCount != b.RowsCount)
                throw new InvalidOperationException();
            
            int n = a.ColumnsCount;
            Matrix<T> result = new Matrix<T>(a.RowsCount, b.ColumnsCount);

            for(int row = 0; row < a.RowsCount; row++)
                for (int column = 0; column < b.ColumnsCount; column++)
                {
                    decimal curValue = 0;
                    for (int i = 0; i < n; i++)
                    {
                        decimal valA = (decimal)Convert.ChangeType(a[row, i], typeof(decimal));
                        decimal valB = (decimal)Convert.ChangeType(b[i, column], typeof(decimal));
                        curValue += valA * valB;
                    }
                    result[row, column] = (T)Convert.ChangeType(curValue, typeof(T));
                }
            return result;
        }
    }
}
