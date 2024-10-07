using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class RGBextract
    {
        public Bitmap getR(Bitmap openImg)
        {
            if(openImg != null)
            {
                Bitmap img = new Bitmap(openImg);
                for(int i =0; i<img.Height; i++)
                {
                    for(int j=0; j<img.Width; j++)
                    {
                        Color RGB = img.GetPixel(j, i);
                        int r = Convert.ToInt32(RGB.R);
                        img.SetPixel(j, i, Color.FromArgb(r, r, r));
                    }
                }
                return img;
            }
            return openImg;
        }

        public Bitmap getG(Bitmap openImg)
        {
            if (openImg != null)
            {
                Bitmap img = new Bitmap(openImg);
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        Color RGB = img.GetPixel(j, i);
                        int g = Convert.ToInt32(RGB.G);
                        img.SetPixel(j, i, Color.FromArgb(g, g, g));
                    }
                }
                return img;
            }
            return openImg;
        }

        public Bitmap getB(Bitmap openImg)
        {
            if (openImg != null)
            {
                Bitmap img = new Bitmap(openImg);
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        Color RGB = img.GetPixel(j, i);
                        int b = Convert.ToInt32(RGB.B);
                        img.SetPixel(j, i, Color.FromArgb(b, b, b));
                    }
                }
                return img;
            }
            return openImg;
        }

        public Bitmap grayScale(Bitmap openImg)
        {
            if (openImg != null)
            {
                Bitmap img = new Bitmap(openImg);
                for(int i=0; i<img.Height; i++)
                {
                    for (int j=0; j<img.Width; j++)
                    {
                        Color RGB = img.GetPixel(j, i);
                        int r = (int)(Convert.ToInt32(RGB.R) * 0.299);
                        int g = (int)(Convert.ToInt32(RGB.G) * 0.587);
                        int b = (int)(Convert.ToInt32(RGB.B) * 0.114);
                        img.SetPixel(j, i, Color.FromArgb(r + g + b, r + g + b, r + g + b));
                    }                
                }
                return img;
            }
            return openImg;
        }
    }
}
