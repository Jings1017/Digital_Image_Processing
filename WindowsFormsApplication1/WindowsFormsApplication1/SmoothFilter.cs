using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class SmoothFilter
    {
        public Bitmap MeanFilter(Bitmap ori, int kernelsize)
        {
            if (ori != null)
            {
                Pedding pedding = new Pedding();
                Bitmap ped = pedding.pedding(ori);
                Bitmap after = new Bitmap(ori.Width, ori.Height);

                for(int y=0; y<ped.Height-kernelsize+1; y++)
                {
                    for(int x=0; x<ped.Width-kernelsize+1; x++)
                    {
                        int r_sum = 0, g_sum = 0, b_sum = 0;
                        for(int i=0; i<kernelsize; i++)
                        {
                            for(int j=0; j<kernelsize; j++)
                            {
                                int r = ped.GetPixel(x + j, y + i).R;
                                int g = ped.GetPixel(x + j, y + i).G;
                                int b = ped.GetPixel(x + j, y + i).B;
                                r_sum += Convert.ToInt32(r);
                                g_sum += Convert.ToInt32(g);
                                b_sum += Convert.ToInt32(b);
                            }
                        }
                        after.SetPixel(x, y, Color.FromArgb((int)(r_sum / Math.Pow(kernelsize, 2)), (int)(g_sum / Math.Pow(kernelsize, 2)), (int)(b_sum / Math.Pow(kernelsize, 2))));
                    }
                }

                return after;
            }
            else
            {
                return ori;
            }
        }

        public Bitmap MedianFilter(Bitmap ori, int kernelsize)
        {
            if (ori != null)
            {
                Pedding pedding = new Pedding();
                Bitmap ped = pedding.pedding(ori);
                Bitmap after = new Bitmap(ori.Width, ori.Height);

                for (int y = 0; y < ped.Height - kernelsize + 1; y++)
                {
                    for (int x = 0; x < ped.Width - kernelsize + 1; x++)
                    {

                        int[] r_sum = new int[kernelsize * kernelsize];
                        int[] g_sum = new int[kernelsize * kernelsize];
                        int[] b_sum = new int[kernelsize * kernelsize];

                        for(int i=0; i<kernelsize; i++)
                        {
                            for(int j=0; j<kernelsize; j++)
                            {
                                int r = ped.GetPixel(x + j, y + i).R;
                                int g = ped.GetPixel(x + j, y + i).G;
                                int b = ped.GetPixel(x + j, y + i).B;
                                r_sum[i * kernelsize + j] = Convert.ToInt32(r);
                                g_sum[i * kernelsize + j] = Convert.ToInt32(g);
                                b_sum[i * kernelsize + j] = Convert.ToInt32(b);
                            }
                        }
                        Array.Sort(r_sum);
                        Array.Sort(g_sum);
                        Array.Sort(b_sum);
                        after.SetPixel(x, y, Color.FromArgb(r_sum[kernelsize * kernelsize / 2], g_sum[kernelsize * kernelsize / 2], b_sum[kernelsize * kernelsize / 2]));
                    }
                }

                return after;
            }
            else
            {
                return ori;
            }
        }
    }
}
