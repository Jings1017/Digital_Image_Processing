using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Stack<Bitmap> undoImgs = new Stack<Bitmap>();
        private Stack<Bitmap> redoImgs = new Stack<Bitmap>();
        private Bitmap openImg;
        private Bitmap openImg2;
        private Bitmap afterImg;
        
        private int pb1_click_count = 0;
        private int pb3_click_count = 0;

        private int[] reg_ori_x = new int[4];
        private int[] reg_ori_y = new int[4];
        private int[] reg_dst_x = new int[4];
        private int[] reg_dst_y = new int[4];

        public Form1()
        {
            InitializeComponent();
            trackBar1.Maximum = 255;
            trackBar2.Maximum = 255;
            trackBar1.Minimum = 1;
            trackBar2.Minimum = 1;
            pictureBox3.Visible = false;
            beforeChart.Visible = true;
            afterChart.Visible = true;
            label1.Visible = false;
            label2.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            // load image
            OpenFileDialog openFIleDialog1 = new OpenFileDialog();
            openFIleDialog1.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg Files(.jpg)|*.jpg|PNG Files(.png)|*.png";
            if (openFIleDialog1.ShowDialog() == DialogResult.OK)
            {
                openImg = new Bitmap(openFIleDialog1.FileName);
                Console.WriteLine(openFIleDialog1.FileName);
                pictureBox1.Image = openImg;
                pictureBox2.Image = openImg;
            }

            undoImgs.Clear();
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // load image 2 
            beforeChart.Visible = false;
            afterChart.Visible = false;
            pictureBox3.Visible = true;

            OpenFileDialog openFIleDialog2 = new OpenFileDialog();
            openFIleDialog2.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg Files(.jpg)|*.jpg|PNG Files(.png)|*.png";
            if (openFIleDialog2.ShowDialog() == DialogResult.OK)
            {
                openImg2 = new Bitmap(openFIleDialog2.FileName);
                Console.WriteLine(openFIleDialog2.FileName);
                pictureBox3.Image = openImg2;
            }

            undoImgs.Clear();
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // save image
            SaveFileDialog savefd = new SaveFileDialog();
            savefd.Filter = "Bitmap Files (.bmp)|*.bmp|Jpeg Files(.jpg)|*.jpg|PNG Files(.png)|*.png";
            if (savefd.ShowDialog() == DialogResult.OK)
            {
                openImg.Save(savefd.FileName);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // undo 
            if (undoImgs.Count == 0)
                return;
            openImg = undoImgs.First();
            pictureBox2.Image = openImg;
            undoImgs.Pop();
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // getR
            Console.WriteLine("R");
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            RGBextract rgbExtract = new RGBextract();
            afterImg = rgbExtract.getR(openImg);

            pictureBox2.Image = afterImg;
            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // getG
            Console.WriteLine("G");
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            RGBextract rgbExtract = new RGBextract();
            afterImg = rgbExtract.getG(openImg);

            pictureBox2.Image = afterImg;
            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // getB
            Console.WriteLine("B");
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            RGBextract rgbExtract = new RGBextract();
            afterImg = rgbExtract.getB(openImg);

            pictureBox2.Image = afterImg;
            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // getGray
            Console.WriteLine("Gray");
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            RGBextract rgbExtract = new RGBextract();
            afterImg = rgbExtract.grayScale(openImg);

            pictureBox2.Image = afterImg;
            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // smoothe filter : mean filter
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            SmoothFilter smooth = new SmoothFilter();
            afterImg = smooth.MeanFilter(openImg, 3);
            pictureBox2.Image = afterImg;
            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // smoothe filter : median filter
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            SmoothFilter smooth = new SmoothFilter();
            afterImg = smooth.MedianFilter(openImg, 3);
            pictureBox2.Image = afterImg;
            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Histogram Equalization 
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            HistogramEqu hisEq = new HistogramEqu();
            afterImg = hisEq.HisEqu(openImg);
            pictureBox2.Image = afterImg;

            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            beforeHis_show();
            afterHis_show();

            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void beforeHis_show()
        {
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            Dictionary<int, int> intesFreq = new Dictionary<int, int>();

            for (int i=0; i< pictureBox1.Image.Height; i++)
            {
                for(int j=0; j<pictureBox1.Image.Width; j++)
                {
                    int grayValue = ((Bitmap)pictureBox1.Image).GetPixel(j, i).R;
                    if (intesFreq.ContainsKey(grayValue))
                    {
                        intesFreq[grayValue]++;
                    }
                    else
                    {
                        intesFreq.Add(grayValue, 1);
                    }
                }
            }

            for(int i=0; i<256; i++)
            {
                if (intesFreq.ContainsKey(i))
                {
                    series.Points.AddXY(i, intesFreq[i]);
                }
                else
                {
                    series.Points.AddXY(i, 0);
                }
            }

            beforeChart.Series.Clear();
            beforeChart.Series.Add(series);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void afterHis_show()
        {
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            Dictionary<int, int> intesFreq = new Dictionary<int, int>();

            for (int i = 0; i < pictureBox2.Image.Height; i++)
            {
                for (int j = 0; j < pictureBox2.Image.Width; j++)
                {
                    int grayValue = ((Bitmap)pictureBox2.Image).GetPixel(j, i).R;
                    if (intesFreq.ContainsKey(grayValue))
                    {
                        intesFreq[grayValue]++;
                    }
                    else
                    {
                        intesFreq.Add(grayValue, 1);
                    }
                }
            }

            for (int i = 0; i < 256; i++)
            {
                if (intesFreq.ContainsKey(i))
                {
                    series.Points.AddXY(i, intesFreq[i]);
                }
                else
                {
                    series.Points.AddXY(i, 0);
                }
            }

            afterChart.Series.Clear();
            afterChart.Series.Add(series);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // threshold button
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            Console.WriteLine(trackBar1.Value);
            Threshold threshold = new Threshold();
            afterImg = threshold.UserDefThreshold(openImg, trackBar1.Value, 255);
            pictureBox2.Image = afterImg;

            undoImgs.Push(openImg);
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            // Sobel vertical
            Sobel sobel = new Sobel();
            afterImg = sobel.Vertical(openImg);
            pictureBox2.Image = afterImg;

            undoImgs.Push(openImg);
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            // sobel horizontal
            Sobel sobel = new Sobel();
            afterImg = sobel.Horizontal(openImg);
            pictureBox2.Image = afterImg;

            undoImgs.Push(openImg);
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            // sobel combined
            Sobel sobel = new Sobel();
            afterImg = sobel.Combined(openImg);
            pictureBox2.Image = afterImg;

            undoImgs.Push(openImg);
            openImg = new Bitmap(afterImg);
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            // sobel threshold 
            Sobel sobel = new Sobel();
            //Bitmap lastImg = undoImgs.First();
            afterImg = sobel.ThresholdSobel(openImg, trackBar2.Value);
            pictureBox2.Image = afterImg;

            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);

            label1.Visible = false;
            label2.Visible = false;
        }

        

        private void button14_Click_1(object sender, EventArgs e)
        {
            beforeChart.Visible = true;
            afterChart.Visible = true;
            pictureBox3.Visible = false;

            // Conected Component 
            Threshold threshold = new Threshold();
            Bitmap th = new Bitmap(openImg);
            th = threshold.UserDefThreshold(openImg, 240, 255);
            ConnectedComponent cc = new ConnectedComponent();
            afterImg = cc.GenerateComponents(th);
            pictureBox2.Image = afterImg;
            int cnt = cc.GetNumOfComp(openImg);
            String str = "Count : " + cnt.ToString();
            label1.Visible = true;
            label2.Visible = false;
            label1.Text = str;

            undoImgs.Push(new Bitmap(openImg));
            openImg = new Bitmap(afterImg);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Registration
            label1.Visible = false;
            label2.Visible = true;
            beforeChart.Visible = false;
            afterChart.Visible = false;
            pictureBox3.Visible = true;


            if (pb1_click_count != 4 || pb3_click_count != 4)
                return;

            Registration reg = new Registration();

            reg.GetOriPoint(reg_ori_x, reg_ori_y);
            reg.GetDstPoint(reg_dst_x, reg_dst_y);

            double[] transitionMatrix = reg.FindTransform().ToList();

            Bitmap regImg = reg.GetRegistrationImage(openImg, openImg2);

            label2.Text = reg.CalculateRegAttr(openImg, (Bitmap)pictureBox2.Image);

            undoImgs.Push(regImg);
            openImg = new Bitmap(regImg);
            pictureBox2.Image = regImg;

            Array.Clear(reg_ori_x, 0, reg_ori_x.Length);
            Array.Clear(reg_ori_y, 0, reg_ori_y.Length);
            Array.Clear(reg_dst_x, 0, reg_dst_x.Length);
            Array.Clear(reg_dst_y, 0, reg_dst_y.Length);
            pb1_click_count = 0;
            pb3_click_count = 0;
        }
        public void drawPoint(int x, int y, PictureBox pb)
        {
            Graphics g = Graphics.FromHwnd(pb.Handle);
            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillEllipse(brush, x - 5, y - 5, 10, 10);
            g.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawPoint(e.X, e.Y, pictureBox1);
            reg_ori_x[pb1_click_count] = e.X;
            reg_ori_y[pb1_click_count] = e.Y;
            pb1_click_count++;
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            drawPoint(e.X, e.Y, pictureBox3);
            reg_dst_x[pb3_click_count] = e.X;
            reg_dst_y[pb3_click_count] = e.Y;
            pb3_click_count++;
        }
    }
}
