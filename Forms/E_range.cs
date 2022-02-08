using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenshinAutoE
{
    public partial class E_range : Form
    {
        public E_range()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112 ;
        public const int SC_MOVE = 0xF010; 
        public const int HTCAPTION = 0x0002;


        private void E_range_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void E_range_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.EAreaLocation = this.Location;
            // Save setting value
            Properties.Settings.Default.Save();
        }

        private void E_range_Paint(object sender, PaintEventArgs e)
        {
            label1.BackColor = Color.Transparent;
            this.Size = new Size(100, 100);
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
    Color.Red, 1, ButtonBorderStyle.Solid, //左边
    Color.Red, 1, ButtonBorderStyle.Solid, //上边
    Color.Red, 1, ButtonBorderStyle.Solid, //右边
    Color.Red, 1, ButtonBorderStyle.Solid);//底边
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void E_range_Load(object sender, EventArgs e)
        {
            this.Location = Properties.Settings.Default.EAreaLocation;
        }
    }
}
