using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Sobel
    {
        private int[] vertical_kernel = new int[] { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
        private int[] horizontal_kernel = new int[] { -1, -2, -1, 0, 0, 0, 1, 2, 1 };

        public Bitmap Vertical(Bitmap src)
        {
            Bitmap res = new Bitmap(src);
            for(int i=1; i < src.Width - 1; i++)
            {
                for(int j=1; j<src.Height-1; j++)
                {
                    int sob_v = 0;
                    for(int mx = -1; mx < 2; mx++)
                    {
                        for(int my=-1; my < 2; my++)
                        {
                            sob_v += src.GetPixel(i + mx, j + my).R * vertical_kernel[(my + 1) * 3 + mx + 1];
                        }
                    }
                    sob_v = sob_v > 255 ? 255 : sob_v;
                    sob_v = sob_v < 0 ? 0 : sob_v;
                    res.SetPixel(i, j, Color.FromArgb(sob_v, sob_v, sob_v));
                }
            }
            return res;
        }

        public Bitmap Horizontal(Bitmap src)
        {
            Bitmap res = new Bitmap(src);
            for (int i = 1; i < src.Width - 1; i++)
            {
                for (int j = 1; j < src.Height - 1; j++)
                {
                    int sob_h = 0;
                    for (int mx = -1; mx < 2; mx++)
                    {
                        for (int my = -1; my < 2; my++)
                        {
                            sob_h += src.GetPixel(i + mx, j + my).R * horizontal_kernel[(my + 1) * 3 + mx + 1];
                        }
                    }
                    sob_h = sob_h > 255 ? 255 : sob_h;
                    sob_h = sob_h < 0 ? 0 : sob_h;
                    res.SetPixel(i, j, Color.FromArgb(sob_h, sob_h, sob_h));
                }
            }
            return res;
        }

        public Bitmap Combined(Bitmap src)
        {

            Bitmap comb = new Bitmap(src);

            for(int i=1; i<src.Width-1; i++)
            {
                for(int j=1; j < src.Height-1; j++)
                {
                    int sob_v = 0;
                    int sob_h = 0;
                    for(int mx = -1; mx < 2; mx++)
                    {
                        for(int my=-1; my < 2; my++)
                        {
                            sob_h += src.GetPixel(i + mx, j + my).R * horizontal_kernel[(mx + 1) * 3 + my + 1];
                            sob_v += src.GetPixel(i + mx, j + my).R * vertical_kernel[(mx + 1) * 3 + my + 1];
                        }
                        
                    }
                    int value = (int)Math.Pow((sob_h * sob_h + sob_v * sob_v), 0.5);
                    value = value > 255 ? 255 : value;
                    value = value < 0 ? 0 : value;
               
                    comb.SetPixel(i, j, Color.FromArgb(value, value, value));

                }
            }

            return comb;
        }

        public Bitmap ThresholdSobel(Bitmap src, int threshold)
        {

            Bitmap comb = new Bitmap(src);

            for (int i = 1; i < src.Width - 1; i++)
            {
                for (int j = 1; j < src.Height - 1; j++)
                {
                    int sob_v = 0;
                    int sob_h = 0;
                    for (int mx = -1; mx < 2; mx++)
                    {
                        for (int my = -1; my < 2; my++)
                        {
                            sob_h += src.GetPixel(i + mx, j + my).R * horizontal_kernel[(mx + 1) * 3 + my + 1];
                            sob_v += src.GetPixel(i + mx, j + my).R * vertical_kernel[(mx + 1) * 3 + my + 1];
                        }

                    }
                    int value = (int)Math.Pow((sob_h * sob_h + sob_v * sob_v), 0.5);
                    value = value > 255 ? 255 : value;
                    value = value < 0 ? 0 : value;


                    comb.SetPixel(i, j, Color.FromArgb(value, value, value));

                }
            }

            Bitmap afterThreshold = new Bitmap(comb);

            for (int i = 0; i < comb.Height; i++)
            {
                for (int j = 0; j < comb.Width; j++)
                {
                    Byte pix = comb.GetPixel(j, i).R;
                    if (pix < threshold) pix = 0;
                    else if (pix >= threshold) pix = (byte)255;
                    afterThreshold.SetPixel(j, i, Color.FromArgb(pix, pix, pix));
                }
            }

            Bitmap res = new Bitmap(src);

            for(int i=0; i<res.Width; i++)
            {
                for(int j=0; j<res.Height; j++)
                {
                    if(afterThreshold.GetPixel(i,j).R == 255)
                    {
                        res.SetPixel(i, j, Color.FromArgb(0,255,0));
                    }
                }
            }

            return res;
        }
    }
}
