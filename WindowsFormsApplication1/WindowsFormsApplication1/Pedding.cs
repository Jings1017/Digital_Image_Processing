using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Pedding
    {
        public Bitmap pedding(Bitmap ori)
        {
            Bitmap ped = new Bitmap(ori.Width + 2, ori.Height + 2);
            for (int y=0; y<ori.Height; y++)
            {
                if (y == 0 || y == ori.Height + 1)
                {
                    for(int x=0; x < ori.Width+2; x++)
                    {
                        ped.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
                else
                {
                    for (int x = 0; x < ori.Width + 2; x++)
                    {
                        if (x == 0 | x == ori.Width + 1)
                        {
                            ped.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            ped.SetPixel(x, y, ori.GetPixel(x - 1, y - 1));
                        }
                    }
                }
            }
            return ped;
        }
    }
}
