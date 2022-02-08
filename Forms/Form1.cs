using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using System.Threading;

namespace GenshinAutoE
{
    public partial class Form1 : Form
    {
        E_range Eform;
        ImageCapture capture = new ImageCapture();
        bool isRunning = false;
        float cmpValue = 0;
        float pushTime;
        Image<Gray, byte> E_img;

        public Form1()
        {
            InitializeComponent();
            try
            {
                E_img = new Image<Gray, byte>("E.png");
            }
            catch
            {
                MessageBox.Show("请放入E.png！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose();
                Close();
            }
            E_img = E_img.Canny(120, 180, 3, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Eform = new E_range();
            Eform.Show();
            Eform.FormClosed += Eform_FormClosed;
            imageBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            numericUpDown1.Value = (decimal)Properties.Settings.Default.cmpMaxValue;
            pushTimeUpDown.Value = (decimal)Properties.Settings.Default.EPushTime;
        }

        private void Eform_FormClosed(object sender, FormClosedEventArgs e)
        {
            Eform = new E_range();
            Eform.Show();
            Eform.FormClosed += Eform_FormClosed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                StartE();
                isRunning = true;
                button1.Text = "关闭自动放E";
                return;
            }
            StopE();
            isRunning = false;
            button1.Text = "启动自动放E";
            imageBox1.Image = null;
        }

        private void StartE()
        {
            int x = Eform.Location.X;
            int y = Eform.Location.Y;
            int w = Eform.Width;
            int h = Eform.Height;
            Eform.Hide();
            capture.Start(x, y, w, h);
            timerCapture.Interval = Convert.ToInt32(1000m / captureFreshUpDown2.Value);
            timerCapture.Start();
        }

        private void StopE()
        {
            timerCapture.Stop();
            capture.Stop();
            Eform.Show();
        }

        private void timerCapture_Tick(object sender, EventArgs e)
        {
            Bitmap img = capture.Capture(false);

            Image<Gray, byte> image = new Image<Gray, byte>(img).Canny(120, 180, 3, true);
            Image<Gray, float> grayImg = new Image<Gray, float>(image.Width, image.Height);
            grayImg = image.MatchTemplate(E_img, TemplateMatchingType.CcorrNormed);
            double min = 0, max = 0;
            Point maxP = new Point(0, 0);
            Point minP = new Point(0, 0);
            CvInvoke.MinMaxLoc(grayImg, ref min, ref max, ref minP, ref maxP);
            Image<Bgr, byte> outimage = new Image<Bgr, byte>(image.Bitmap);
            if (max > cmpValue) //最大匹配度
            {
                CvInvoke.Rectangle(outimage, new Rectangle(maxP, new Size(E_img.Width, E_img.Height)), new MCvScalar(0, 0, 255), 3);
                new Thread(() =>
                {
                    E_event();
                }).Start();
            }
            //label1.Text = $"min: {min} max: {max}";
            statusStrip1.Items[0].Text = $"max: {max}";

            imageBox1.Image = outimage;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            cmpValue = (float)numericUpDown1.Value;
            Properties.Settings.Default.cmpMaxValue = cmpValue;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.EAreaLocation = Eform.Location;
            Properties.Settings.Default.Save();
        }

        private void pushTimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            pushTime = (float)pushTimeUpDown.Value;
            Properties.Settings.Default.EPushTime = pushTime;
        }

        private void E_event()
        {
            Utils.DownKey(69);
            Thread.Sleep((int)pushTime * 1000);
            Utils.UpKey(69);
        }

        private void captureFreshUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.captureFreshTime = (int)captureFreshUpDown2.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImagePreview ip = new ImagePreview(E_img);
            ip.Show();
        }
    }
}
