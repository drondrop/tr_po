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
        public Form1()
        {
            InitializeComponent();
            //pictureBox2.DataBindings= new ControlBindingsCollection()
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iProcc.LoadFromFile();
            pictureBox1.Image = iProcc.CurrentImage;
            iProcc.SaveToFile();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            
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
            //OilPainting --- нормальный Sepia

            pictureBox2.Image = iProcc.DoWithUndo(new Filter_Command(new AForge.Imaging.Filters.OilPainting() ));
        }
    }
}
