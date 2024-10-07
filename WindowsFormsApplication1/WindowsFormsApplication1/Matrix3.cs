using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Matrix3
    {
        double[] matrix = new double[9];
        public int mode;
        public Matrix3(double[] a, int len)
        {
            mode = len;
            for(int i=0; i< len; i++)
            {
                matrix[i] = a[i];
            }
        }

        public double[] ToList()
        {
            double[] a = new double[mode];
            
            for(int i=0; i<mode; i++)
            {
                a[i] = matrix[i];
            }

            return a;
        }

        public double GetValue(int y, int x)
        {
            return matrix[y * 3 + x];
        }

        public Matrix3 Mut(Matrix3 x)
        {
            double[] b = new double[x.mode];
            switch (x.mode)
            {
                case 3:
                    for(int i=0; i<mode/3; i++)
                    {
                        double rowSum = 0;
                        for(int j=0; j<3; j++)
                        {
                            rowSum += x.GetValue(0, j) * GetValue(i, j);
                        }
                        b[i] = rowSum;
                        rowSum = 0;
                    }
                    break;
                case 9:
                    for (int i = 0 ; i < mode/3; i++)
                    {
                        for(int j=0; j < 3; j++)
                        {
                            double rowSum = 0;
                            for(int k=0; k<3; k++)
                            {
                                rowSum += GetValue(i, k) * x.GetValue(k, j);
                            }
                            b[i * 3 + j] = rowSum;
                            rowSum = 0;
                        }
                    }
                    break;
            }
            Matrix3 mat = new Matrix3(b, x.mode);
            return mat;
        }

        public Matrix3 Transpose()
        {
            double[] trans = new double[]
            {
                matrix[0], matrix[3], matrix[6],
                matrix[1], matrix[4], matrix[7],
                matrix[2], matrix[5], matrix[8],
            };
            return new Matrix3(trans, 9);

        }

        public Matrix3 Inverse()
        {
            var e = matrix[4];
            var i = matrix[8];
            var f = matrix[5];
            var h = matrix[7];
            var d = matrix[3];
            var g = matrix[6];
            var b = matrix[1];
            var c = matrix[2];
            var a = matrix[0];
            double[] inv = new double[]
            {
                e * i - f * h,
                -d * i + f * g,
                d * h - e * g,
                -b * i + c * h,
                a * i - c * g,
                -a * h + b * g,
                b * f - c * e,
                -a * f + c * d,
                a * e - b * d,
            };

            inv = new Matrix3(inv, 9).Transpose().ToList();

            double det = a * (e * i - f * h) +
                      b * (-d * i + f * g) +
                      c * (d * h - e * g);
            for (int v = 0; v < 9; v++)
            {
                inv[v] /= det;
            }

            return new Matrix3(inv, 9);
        }


        public double Determinant()
        {
            var e = matrix[4];
            var i = matrix[8];
            var f = matrix[5];
            var h = matrix[7];
            var d = matrix[3];
            var g = matrix[6];
            var b = matrix[1];
            var c = matrix[2];
            var a = matrix[0];

            double det = a * (e * i - f * h) +
                      b * (-d * i + f * g) +
                      c * (d * h - e * g);
            

            return det;
        }
    }
}
