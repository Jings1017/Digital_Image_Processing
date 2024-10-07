using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Threshold
    {
        public Bitmap UserDefThreshold(Bitmap src, int threshold_value, int max = 255)
        {
            Bitmap after = new Bitmap(src);

            for (int i=0; i< src.Height; i++)
            {
                for(int j=0; j < src.Width; j++)
                {
                    Byte pix = src.GetPixel(j, i).R;
                    if (pix < threshold_value) pix = 0;
                    else if (pix >= threshold_value) pix = (byte)max;
                    after.SetPixel(j, i, Color.FromArgb(pix, pix, pix));
                }
            }
            return after;
        }
    }
}
