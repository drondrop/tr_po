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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);

                
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

                BrightnessCorrection filter = new BrightnessCorrection(trackBar1.Value-50);
                pictureBox2.Image = filter.Apply((Bitmap)pictureBox1.Image);
        }
    }
}
