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
    public partial class ImagePreview : Form
    {
        public ImagePreview(Image<Gray, byte> img)
        {
            InitializeComponent();
            imageBox1.Image = img;
        }

    }
}
