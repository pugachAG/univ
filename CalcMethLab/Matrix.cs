using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcMethLab
{
    public struct Matrix
    {
        private double[,] data;
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        public Matrix(int rowCount, int columnCount)
            : this()
        {
            this.data = new double[rowCount, columnCount];
            this.RowCount = rowCount;
            this.ColumnCount = columnCount;
        }

        public Matrix(Matrix M)
            : this(M.RowCount, M.ColumnCount)
        {
            for(int i = 0; i < RowCount; i++)
                for(int j = 0; j < ColumnCount; j++)
                    this[i,j] = M[i,j];
        }

        public Matrix Transpose()
        {
            Matrix res = new Matrix(ColumnCount, RowCount);
            for (int i = 0; i < ColumnCount; i++)
                for (int j = 0; j < RowCount; j++)
                    res[i, j] = this[j, i];
            return res;
        }

        public Matrix(double[] d)
           : this((int)Math.Sqrt(d.Length), (int)Math.Sqrt(d.Length))
        {
            for (int i = 0; i < this.RowCount; i++)
                for (int j = 0; j < this.ColumnCount; j++)
                    this[i, j] = d[j + this.ColumnCount * i];
        }

        public double this[int i, int j]
        {
            get
            {
                return this.data[i, j];
            }
            set
            {
                this.data[i, j] = value;
            }
        }

        public double[,] Data
        {
            get
            {
                return this.data;
            }
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            Matrix result = new Matrix(A);
            for (int i = 0; i < A.RowCount; i++)
                for (int j = 0; j < A.ColumnCount; j++)
                    result[i, j] += B[i, j];
            return result;
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            Matrix result = new Matrix(A);
            for (int i = 0; i < A.RowCount; i++)
                for (int j = 0; j < A.ColumnCount; j++)
                    result[i, j] -= B[i, j];
            return result;
            
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            Matrix result = new Matrix(A.RowCount, B.ColumnCount);
            for(int i = 0; i <A.RowCount; i++)
                for (int j = 0; j < B.ColumnCount; j++)
                {
                    double a = 0;
                    for (int r = 0; r < A.ColumnCount; r++)
                        a += A[i, r] * B[r, j];
                    result[i, j] = a;
                }
            return result;            
        }

    }

}
