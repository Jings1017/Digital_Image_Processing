using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Registration
    {
        private const int point_n = 4;
        int[] origin_x = new int[point_n];
        int[] origin_y = new int[point_n];
        int[] after_x = new int[point_n];
        int[] after_y = new int[point_n];

        private Matrix3 transform;

        private double[] cx = new double[4];
        private double[] cy = new double[4];

        private double diff = 0;

        public void GetOriPoint(int[] ori_x, int[] ori_y)
        {
            origin_x = ori_x;
            origin_y = ori_y;

            Console.WriteLine("get ori");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(origin_x[i] + " " + origin_y[i]);
            }
        }

        public void GetDstPoint(int[] dst_x, int[] dst_y)
        {
            after_x = dst_x;
            after_y = dst_y;

            Console.WriteLine("get dst");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(after_x[i] + " " + after_y[i]);
            }
        }

        public Bitmap GetRegistrationImage(Bitmap openImg , Bitmap openImg2)
        {

            double[,] matrix_x = new double[4, 5], matrix_y = new double[4, 5];
            for (int i = 0; i < 4; i++)
            {
                matrix_x[i, 0] = matrix_y[i, 0] = origin_x[i];
                matrix_x[i, 1] = matrix_y[i, 1] = origin_y[i];
                matrix_x[i, 2] = matrix_y[i, 2] = origin_x[i] * origin_y[i];
                matrix_x[i, 3] = matrix_y[i, 3] = 1;
                matrix_x[i, 4] = after_x[i];
                matrix_y[i, 4] = after_y[i];
            }
            gauss_jordan(matrix_x);
            gauss_jordan(matrix_y);
            Bitmap changedImg = new Bitmap(openImg.Width, openImg.Height);

            double v, w;
            for (int x = 0; x < changedImg.Width; x++)
            {
                for (int y = 0; y < changedImg.Height; y++)
                {
                    v = matrix_x[0, 4] * x + matrix_x[1, 4] * y + matrix_x[2, 4] * x * y + matrix_x[3, 4];
                    w = matrix_y[0, 4] * x + matrix_y[1, 4] * y + matrix_y[2, 4] * x * y + matrix_y[3, 4];
                    if (v < 0 || v > openImg2.Width - 1 || w < 0 || w > openImg2.Height - 1)
                        changedImg.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    else
                    {
                        int color;
                        color = (int)(openImg2.GetPixel((int)Math.Floor(v), (int)Math.Floor(w)).R * (Math.Floor(v) + 1 - v) * (Math.Floor(w) + 1 - w) +
                                openImg2.GetPixel((int)Math.Floor(v), (int)Math.Ceiling(w)).R * (Math.Floor(v) + 1 - v) * (w - Math.Floor(w)) +
                                openImg2.GetPixel((int)Math.Ceiling(v), (int)Math.Floor(w)).R * (v - Math.Floor(v)) * (Math.Floor(w) + 1 - w) +
                                openImg2.GetPixel((int)Math.Ceiling(v), (int)Math.Ceiling(w)).R * (v - Math.Floor(v)) * (w - Math.Floor(w)));
                        changedImg.SetPixel(x, y, Color.FromArgb(color, color, color));
                    }
                }
            }
            diff = 0;
            for (int x = 0; x < changedImg.Width; x++)
                for (int y = 0; y < changedImg.Height; y++)
                    diff += (1.0f / (changedImg.Width * changedImg.Height)) * Math.Abs(changedImg.GetPixel(x, y).R - openImg.GetPixel(x, y).R);
            return changedImg;
        }

        void gauss_jordan(double[,] matrix)
        {
            double multiplier;
            int row = matrix.GetLength(0), col = matrix.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                multiplier = 1 / matrix[i, i];
                for (int j = 0; j < col; j++)
                    matrix[i, j] *= multiplier;

                for (int j = i + 1; j < row; j++)
                {
                    multiplier = matrix[j, i];
                    for (int k = 0; k < col; k++)
                        matrix[j, k] -= multiplier * matrix[i, k];
                }
            }

            for (int i = row - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    multiplier = matrix[j, i];
                    for (int k = 0; k < col; k++)
                        matrix[j, k] -= multiplier * matrix[i, k];
                }
            }
        }

        public Matrix3 FindTransform()
        {
            Matrix3 ori = new Matrix3(new double[]{origin_x[0], origin_x[1], origin_x[2],
                                                            origin_y[0], origin_y[1], origin_y[2],
                                                            1, 1, 1}, 9);
            Matrix3 aft = new Matrix3(new double[]{after_x[0], after_x[1], after_x[2],
                after_y[0], after_y[1], after_y[2],
                1, 1, 1}, 9);

            transform = aft.Mut(ori.Inverse());
            
            return transform;
        }

        double CalculateZoomX()
        {
            return Math.Sqrt(Math.Pow(after_x[0] - after_x[1], 2) + Math.Pow(after_y[0] - after_y[1], 2)) /
                Math.Sqrt(Math.Pow(origin_x[0] - origin_x[1], 2) + Math.Pow(origin_y[0] - origin_y[1], 2));
        }

        double CalculateZoomY()
        {
            return Math.Sqrt(Math.Pow(after_x[0] - after_x[2], 2) + Math.Pow(after_y[0] - after_y[2], 2)) /
                   Math.Sqrt(Math.Pow(origin_x[0] - origin_x[2], 2) + Math.Pow(origin_y[0] - origin_y[2], 2));
        }

        public string CalculateRegAttr(Bitmap refer, Bitmap reg)
        {
            double[] scalefac = CalculateScaleFactor();
            string s = string.Format("Scaling Factor x: {0:N2}\nScaling Factor y: {1:N2}\nCosine Angle theta: {2:N2}\nIntensity Difference: {3:N2}\n", 1/scalefac[0], 1/scalefac[1], CalculateCosAngle(), diff);
            return s;
        }

        public double[] CalculateScaleFactor()
        {
            double[] scal = ScalingMatrix().ToList();
            return new double[] { scal[0], scal[4] };
        }

        public double CalculateCosAngle()
        {
            Matrix3 rotate = RotateMatrix();
            double cosAngle = (rotate.ToList()[0] + rotate.ToList()[4]) / 2;
            return Radian2Degree(Math.Acos(cosAngle));
        }

        double Radian2Degree(double rad)
        {
            return rad * 180 / Math.PI;
        }

        Matrix3 RotateMatrix()
        {
            return TranslateMatrix().Inverse().Mut(ScalingMatrix().Inverse()).Mut(FindTransform());
        }

        Matrix3 ScalingMatrix()
        {
            return new Matrix3(new double[] { CalculateZoomX(), 0, 0, 0, CalculateZoomY(), 0, 0, 0, 1 }, 9);
        }

        Matrix3 TranslateMatrix()
        {
            return new Matrix3(new double[] { 1, 0, transform.ToList()[2], 0, 1, transform.ToList()[5], 0, 0, 1 }, 9);
        }
    }

}
