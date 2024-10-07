using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Matrix4
    {
        double[] matrix = new double[16];
        public int mode;
        public Matrix4(double[] a)
        {
            for (int i = 0; i < 16; i++)
            {
                matrix[i] = a[i];
            }
        }

        public double GetValue(int y, int x)
        {
            return matrix[y * 4 + x];
        }

        public Matrix4 Inverse(Matrix4 ori)
        {
            double[] inv = new double[16];
            
            double det = 0;
            

            inv[0] =   ori.GetValue(1, 1) * ori.GetValue(2, 2) * ori.GetValue(3, 3) -
                       ori.GetValue(1, 1) * ori.GetValue(2, 3) * ori.GetValue(3, 2) -
                       ori.GetValue(2, 1) * ori.GetValue(1, 2) * ori.GetValue(3, 3) +
                       ori.GetValue(2, 1) * ori.GetValue(1, 3) * ori.GetValue(3, 2) +
                       ori.GetValue(3, 1) * ori.GetValue(1, 2) * ori.GetValue(2, 3) -
                       ori.GetValue(3, 1) * ori.GetValue(1, 3) * ori.GetValue(2, 2);

            inv[4] = - ori.GetValue(1, 0) * ori.GetValue(2, 2) * ori.GetValue(3, 3) +
                       ori.GetValue(1, 0) * ori.GetValue(2, 3) * ori.GetValue(3, 2) +
                       ori.GetValue(2, 0) * ori.GetValue(1, 2) * ori.GetValue(3, 3) -
                       ori.GetValue(2, 0) * ori.GetValue(1, 3) * ori.GetValue(3, 2) -
                       ori.GetValue(3, 0) * ori.GetValue(1, 2) * ori.GetValue(2, 3) +
                       ori.GetValue(3, 0) * ori.GetValue(1, 3) * ori.GetValue(2, 2);

            inv[8] =   ori.GetValue(1, 0) * ori.GetValue(2, 1) * ori.GetValue(3, 3) +
                       ori.GetValue(1, 0) * ori.GetValue(2, 3) * ori.GetValue(3, 1) -
                       ori.GetValue(2, 0) * ori.GetValue(1, 1) * ori.GetValue(3, 3) +
                       ori.GetValue(2, 0) * ori.GetValue(1, 3) * ori.GetValue(3, 1) +
                       ori.GetValue(3, 0) * ori.GetValue(1, 1) * ori.GetValue(2, 3) -
                       ori.GetValue(3, 0) * ori.GetValue(1, 3) * ori.GetValue(2, 1);

            inv[12] = -ori.GetValue(1, 0) * ori.GetValue(2, 1) * ori.GetValue(3, 2) +
                       ori.GetValue(1, 0) * ori.GetValue(2, 2) * ori.GetValue(3, 1) +
                       ori.GetValue(2, 0) * ori.GetValue(1, 1) * ori.GetValue(3, 2) -
                       ori.GetValue(2, 0) * ori.GetValue(1, 2) * ori.GetValue(3, 1) -
                       ori.GetValue(3, 0) * ori.GetValue(1, 1) * ori.GetValue(2, 2) +
                       ori.GetValue(3, 0) * ori.GetValue(1, 2) * ori.GetValue(2, 1);

            inv[12] = -ori.GetValue(0, 1) * ori.GetValue(2, 2) * ori.GetValue(3, 3) +
                       ori.GetValue(0, 1) * ori.GetValue(2, 3) * ori.GetValue(3, 2) +
                       ori.GetValue(2, 1) * ori.GetValue(0, 2) * ori.GetValue(3, 3) -
                       ori.GetValue(2, 1) * ori.GetValue(0, 3) * ori.GetValue(3, 2) -
                       ori.GetValue(3, 1) * ori.GetValue(0, 2) * ori.GetValue(2, 3) +
                       ori.GetValue(3, 1) * ori.GetValue(0, 3) * ori.GetValue(2, 2);

            inv[5] =   ori.GetValue(0, 0) * ori.GetValue(2, 2) * ori.GetValue(3, 3) -
                       ori.GetValue(0, 0) * ori.GetValue(2, 3) * ori.GetValue(3, 2) -
                       ori.GetValue(2, 0) * ori.GetValue(0, 2) * ori.GetValue(3, 3) +
                       ori.GetValue(2, 0) * ori.GetValue(0, 3) * ori.GetValue(3, 2) +
                       ori.GetValue(3, 0) * ori.GetValue(0, 2) * ori.GetValue(2, 3) -
                       ori.GetValue(3, 0) * ori.GetValue(0, 3) * ori.GetValue(2, 2);

            inv[9] =   ori.GetValue(0, 0) * ori.GetValue(2, 1) * ori.GetValue(3, 3) +
                       ori.GetValue(0, 0) * ori.GetValue(2, 3) * ori.GetValue(3, 1) +
                       ori.GetValue(2, 0) * ori.GetValue(0, 1) * ori.GetValue(3, 3) -
                       ori.GetValue(2, 0) * ori.GetValue(0, 3) * ori.GetValue(3, 1) -
                       ori.GetValue(3, 0) * ori.GetValue(0, 1) * ori.GetValue(2, 3) +
                       ori.GetValue(3, 0) * ori.GetValue(0, 3) * ori.GetValue(2, 1);

            inv[13] =  ori.GetValue(0, 0) * ori.GetValue(2, 1) * ori.GetValue(3, 2) -
                       ori.GetValue(0, 0) * ori.GetValue(2, 2) * ori.GetValue(3, 1) -
                       ori.GetValue(2, 0) * ori.GetValue(0, 1) * ori.GetValue(3, 2) +
                       ori.GetValue(2, 0) * ori.GetValue(0, 2) * ori.GetValue(3, 1) +
                       ori.GetValue(3, 0) * ori.GetValue(0, 1) * ori.GetValue(2, 2) -
                       ori.GetValue(3, 0) * ori.GetValue(0, 2) * ori.GetValue(2, 1);

            inv[2] =   ori.GetValue(0, 1) * ori.GetValue(2, 2) * ori.GetValue(3, 3) -
                       ori.GetValue(0, 1) * ori.GetValue(1, 3) * ori.GetValue(3, 2) -
                       ori.GetValue(1, 1) * ori.GetValue(0, 2) * ori.GetValue(3, 3) +
                       ori.GetValue(1, 1) * ori.GetValue(0, 3) * ori.GetValue(3, 2) +
                       ori.GetValue(3, 1) * ori.GetValue(0, 2) * ori.GetValue(1, 3) -
                       ori.GetValue(3, 1) * ori.GetValue(0, 3) * ori.GetValue(1, 2);

            inv[6] = - ori.GetValue(0, 0) * ori.GetValue(2, 2) * ori.GetValue(3, 3) +
                       ori.GetValue(0, 0) * ori.GetValue(1, 3) * ori.GetValue(3, 2) +
                       ori.GetValue(1, 0) * ori.GetValue(0, 2) * ori.GetValue(3, 3) -
                       ori.GetValue(1, 0) * ori.GetValue(0, 3) * ori.GetValue(3, 2) -
                       ori.GetValue(3, 0) * ori.GetValue(0, 2) * ori.GetValue(1, 3) +
                       ori.GetValue(3, 0) * ori.GetValue(0, 3) * ori.GetValue(1, 2);

            inv[10] = -ori.GetValue(0, 0) * ori.GetValue(1, 1) * ori.GetValue(3, 3) -
                       ori.GetValue(0, 0) * ori.GetValue(1, 3) * ori.GetValue(3, 1) -
                       ori.GetValue(1, 0) * ori.GetValue(0, 1) * ori.GetValue(3, 3) +
                       ori.GetValue(1, 0) * ori.GetValue(0, 3) * ori.GetValue(3, 1) +
                       ori.GetValue(3, 0) * ori.GetValue(0, 1) * ori.GetValue(1, 3) -
                       ori.GetValue(3, 0) * ori.GetValue(0, 3) * ori.GetValue(1, 1);

            inv[14] = -ori.GetValue(0, 0) * ori.GetValue(1, 1) * ori.GetValue(3, 2) +
                       ori.GetValue(0, 0) * ori.GetValue(1, 2) * ori.GetValue(3, 1) +
                       ori.GetValue(1, 0) * ori.GetValue(0, 1) * ori.GetValue(3, 2) -
                       ori.GetValue(1, 0) * ori.GetValue(0, 2) * ori.GetValue(3, 1) -
                       ori.GetValue(3, 0) * ori.GetValue(0, 1) * ori.GetValue(1, 2) +
                       ori.GetValue(3, 0) * ori.GetValue(0, 2) * ori.GetValue(1, 1);

            inv[3] = - ori.GetValue(0, 1) * ori.GetValue(1, 2) * ori.GetValue(2, 3) +
                       ori.GetValue(0, 1) * ori.GetValue(1, 3) * ori.GetValue(2, 2) +
                       ori.GetValue(1, 1) * ori.GetValue(0, 2) * ori.GetValue(2, 3) -
                       ori.GetValue(1, 1) * ori.GetValue(0, 3) * ori.GetValue(2, 2) -
                       ori.GetValue(2, 1) * ori.GetValue(0, 2) * ori.GetValue(1, 3) +
                       ori.GetValue(2, 1) * ori.GetValue(0, 3) * ori.GetValue(1, 2);

            inv[7] =   ori.GetValue(0, 0) * ori.GetValue(1, 2) * ori.GetValue(2, 3) -
                       ori.GetValue(0, 0) * ori.GetValue(1, 3) * ori.GetValue(2, 2) -
                       ori.GetValue(1, 0) * ori.GetValue(0, 2) * ori.GetValue(2, 3) +
                       ori.GetValue(1, 0) * ori.GetValue(0, 3) * ori.GetValue(2, 2) +
                       ori.GetValue(2, 0) * ori.GetValue(0, 2) * ori.GetValue(1, 3) -
                       ori.GetValue(2, 0) * ori.GetValue(0, 3) * ori.GetValue(1, 2);

            inv[11] =  ori.GetValue(0, 0) * ori.GetValue(1, 1) * ori.GetValue(2, 3) +
                       ori.GetValue(0, 0) * ori.GetValue(1, 3) * ori.GetValue(2, 1) +
                       ori.GetValue(1, 0) * ori.GetValue(0, 1) * ori.GetValue(2, 3) -
                       ori.GetValue(1, 0) * ori.GetValue(0, 3) * ori.GetValue(2, 1) -
                       ori.GetValue(2, 0) * ori.GetValue(0, 1) * ori.GetValue(1, 3) +
                       ori.GetValue(2, 0) * ori.GetValue(0, 3) * ori.GetValue(1, 1);

            inv[15] = ori.GetValue(0, 0) * ori.GetValue(1, 1) * ori.GetValue(2, 2) -
                       ori.GetValue(0, 0) * ori.GetValue(1, 2) * ori.GetValue(2, 1) -
                       ori.GetValue(1, 0) * ori.GetValue(0, 1) * ori.GetValue(2, 2) +
                       ori.GetValue(1, 0) * ori.GetValue(0, 2) * ori.GetValue(2, 1) +
                       ori.GetValue(2, 0) * ori.GetValue(0, 1) * ori.GetValue(1, 2) -
                       ori.GetValue(2, 0) * ori.GetValue(0, 2) * ori.GetValue(1, 1);

            det = ori.GetValue(0, 0) * inv[0] + ori.GetValue(0, 1) * inv[4] + ori.GetValue(0, 2) * inv[8] + ori.GetValue(0, 3) * inv[12];

            det = 1 / det;

            double[] tmp = new double[16];

            for(int i=0; i < 16; i++)
            {
                tmp[i] = inv[i] * det;
            }

            Matrix4 res = new Matrix4(tmp);
 
            return res;
        }

        public Matrix4 Multiply(Matrix4 ori1, Matrix4 ori2)
        {
            double[] tmp = new double[16];
            

            for(int i=0; i < 4; i++)
            {
                for(int j=0; j < 4; j++)
                {
                    double tmp_sum = 0;
                    for(int k=0; k < 4; k++)
                    {
                        tmp_sum += ori1.GetValue(i, k) * ori2.GetValue(k, j);
                    }
                    tmp[i * 4 + j] = tmp_sum;
                    tmp_sum = 0;
                }
            }

            Matrix4 res = new Matrix4(tmp);
            return res;
        }
    }
}
