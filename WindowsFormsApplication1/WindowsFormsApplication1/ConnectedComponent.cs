using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class ConnectedComponent
    {
        private int numOfComp = 0;
        public int GetNumOfComp(Bitmap src)
        {
            return numOfComp;
        }

        public Bitmap GenerateComponents(Bitmap src)
        {
            int[,] array = new int[src.Width, src.Height];

            for(int i=0; i < src.Width; i++)
            {
                for(int j=0; j<src.Height; j++)
                {
                    if (src.GetPixel(i, j).R == 255)
                        array[i, j] = 255;
                    else
                        array[i, j] = 0;
                }
            }

            int labelCnt = 1;

            
            for (int x = 0; x < src.Width; x++)
            {
                for (int y = 0; y < src.Height; y++)
                {
                    int pic = src.GetPixel(x, y).R;
                    if(pic != 255) // if black 
                    {
                        
                        // (0,0)
                        if(x==0 && y == 0)
                        {
                            array[x, y] = labelCnt;
                            labelCnt++;
                        }
                        // top
                        else if(y==0 && x != 0)
                        {
                            if (array[x - 1, y] != 255)
                            {
                                array[x, y] = array[x - 1, y];
                            }
                            else
                            {
                                array[x, y] = labelCnt;
                                labelCnt++;
                            }

                        }
                        // left
                        else if(x==0 && y != 0)
                        {
                            if (array[x, y - 1] != 255)
                            {
                                array[x, y] = array[x, y - 1];
                            }
                            else
                            {
                                array[x, y] = labelCnt;
                                labelCnt++;
                            }
                        }
                        else
                        {
                            if (array[x, y - 1] != 255 && array[x - 1, y] == 255)
                            {
                                array[x, y] = array[x, y - 1];
                            }
                            else if(array[x-1,y]!=255 && array[x, y - 1] == 255)
                            {
                                array[x, y] = array[x - 1, y];
                            }
                            else if(array[x-1,y]!=255 && array[x, y - 1] != 255)
                            {
                                array[x, y] = array[x, y - 1];
                                int topLabel = array[x, y - 1];
                                int leftLabel = array[x - 1, y];
                                if(topLabel != leftLabel)
                                {
                                    for(int i=0; i < src.Width; i++)
                                    {
                                        for(int j=0; j < src.Height; j++)
                                        {
                                            if(array[i,j] == leftLabel)
                                            {
                                                array[i, j] = topLabel;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                array[x, y] = labelCnt;
                                labelCnt++;
                            }
                        }
                    }
                    else
                    {
                        array[x, y] = 255;
                    }
                }
            }



           for(int time=0; time<2; time++)
           {
                for (int i = 1; i < src.Width; i++)
                {
                    for (int j = 1; j < src.Height; j++)
                    {
                        if (array[i, j] != 255)
                        {
                            array[i, j] = Math.Min(array[i, j], array[i, j - 1]);
                            array[i, j] = Math.Min(array[i, j], array[i - 1, j - 1]);
                            array[i, j] = Math.Min(array[i, j], array[i - 1, j]);
                        }
                    }
                }

                for (int i = src.Width - 2; i >= 0; i--)
                {
                    for (int j = 1; j < src.Height; j++)
                    {
                        if (array[i, j] != 255)
                        {
                            array[i, j] = Math.Min(array[i, j], array[i, j - 1]);
                            array[i, j] = Math.Min(array[i, j], array[i + 1, j - 1]);
                            array[i, j] = Math.Min(array[i, j], array[i + 1, j]);
                        }
                    }
                }

                for (int i = src.Width - 2; i >= 0; i--)
                {
                    for (int j = src.Height - 2; j >= 0; j--)
                    {
                        if (array[i, j] != 255)
                        {
                            array[i, j] = Math.Min(array[i, j], array[i, j + 1]);
                            array[i, j] = Math.Min(array[i, j], array[i + 1, j + 1]);
                            array[i, j] = Math.Min(array[i, j], array[i + 1, j]);
                        }
                    }
                }

                for (int i = 1; i < src.Width; i++)
                {
                    for (int j = src.Height - 2; j >= 0; j--)
                    {
                        if (array[i, j] != 255)
                        {
                            array[i, j] = Math.Min(array[i, j], array[i, j + 1]);
                            array[i, j] = Math.Min(array[i, j], array[i - 1, j + 1]);
                            array[i, j] = Math.Min(array[i, j], array[i - 1, j]);
                        }
                    }
                }
            }

            

            List<int> index = new List<int>();
            
            for(int i=0; i < src.Width; i++)
            {
                for(int j=0; j<src.Height; j++)
                {
                    if (array[i, j] != 255)
                    {
                        Boolean check = false;
                        for(int k=0; k < index.Count; k++)
                        {
                            if (array[i, j] == index[k])
                                check = true;
                        }
                        if(!check)
                            index.Add(array[i, j]);
                    }
                }
            }

            numOfComp = index.Count;

            Bitmap CC = new Bitmap(src);
            Random r = new Random(Guid.NewGuid().GetHashCode());
            for (int color=0; color < index.Count; color++)
            {
                
                Color curColor = Color.FromArgb(r.Next(0, 256),r.Next(0, 256), r.Next(0, 256));

                for(int x=0; x<CC.Width; x++)
                {
                    for(int y=0; y<CC.Height; y++)
                    {
                        if (array[x, y] == index[color])
                        {
                            CC.SetPixel(x, y, curColor);
                        }
                    }
                }
            }
            return CC;
        }
    }
}
