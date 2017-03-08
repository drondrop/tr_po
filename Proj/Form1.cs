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
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = iProcc.LoadFromFile(openFileDialog1.FileName);

                
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            
               // BrightnessCorrection filter = new BrightnessCorrection(trackBar1.Value-50);
            pictureBox2.Image = iProcc.DoPreView(new Filter_Command(new BrightnessCorrection(trackBar1.Value - 50)));
                //pictureBox2.Image = filter.Apply((Bitmap)pictureBox1.Image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = iProcc.UnDo((Bitmap)pictureBox2.Image);
        }

        private void trackBar1_MouseCaptureChanged(object sender, EventArgs e)
        {
            pictureBox2.Image = iProcc.DoWithUndo(new Filter_Command(new AForge.Imaging.Filters.BrightnessCorrection(trackBar1.Value - 50)));
        }
    }
}
