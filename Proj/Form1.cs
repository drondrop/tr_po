using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using AForge;
using AForge.Imaging.Filters;



namespace Proj
{
    public partial class Form1 : Form
    {
        ImageProcessWin iProcc = new ImageProcessWin();
        List<ICFilter> filters_correction = new List<ICFilter>()
        {
            new Brightness_Correction(),
            new Contrast_Correction(),
            new HueModifier_Correction(),
            new Saturation_Correction()
        };
        public Form1()
        {
            InitializeComponent();
            //pictureBox2.DataBindings= new ControlBindingsCollection()
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iProcc.LoadFromFile();
            pictureBox1.Image = iProcc.CurrentImage;
           // iProcc.SaveToFile();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            double mapping = (double)trackBar1.Value * 0.01;//(double)trackBar1.Value
            //OilPainting --- нормальный Sepia
            //HueModifier filter = new AForge.Imaging.Filters.HueModifier((int)(359 * mapping));
            int i = 3;
            filters_correction[i].param = mapping;
            pictureBox2.Image = iProcc.DoPreView(new Filter_Command(filters_correction[i]));
               // BrightnessCorrection filter = new BrightnessCorrection(trackBar1.Value-50);
            //pictureBox2.Image = iProcc.DoPreView(new Filter_Command(new BrightnessCorrection(trackBar1.Value - 50)));
                //pictureBox2.Image = filter.Apply((Bitmap)pictureBox1.Image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = iProcc.UnDo((Bitmap)pictureBox2.Image);
        }

        private void trackBar1_MouseCaptureChanged(object sender, EventArgs e)
        {
            //double mapping = (double)trackBar1.Value *0.01;
            ////OilPainting --- нормальный Sepia
            //HueModifier filter = new AForge.Imaging.Filters.HueModifier((int)(359*mapping));
            //pictureBox2.Image = iProcc.DoWithUndo(new Filter_Command_(filter));
        }
    }
}
