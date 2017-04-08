using Proj.Command;
using Proj.ProcessImage;
using System;
using System.Drawing;
using System.Windows.Forms;



namespace Proj
{
    public partial class Form1 : Form
    {
        ImageProcessWin iProcc = new ImageProcessWin();
        
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.LargeIcon;
            listView1.Columns.Add("1");
            //pictureBox2.DataBindings= new ControlBindingsCollection()
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iProcc.LoadFromFile();
            pictureBox1.Image = iProcc.CurrentImage;
            init();
           // iProcc.SaveToFile();
        }
        private void init()
        {
            
            listView1.LargeImageList = iProcc.GetImageFilters();
            int tt=0;
            foreach (var t in iProcc.filterNames)
            {
                listView1.Items.Add(t, tt);
                    tt++;
            }
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            double mapping = (double)trackBar1.Value * 0.01;//(double)trackBar1.Value
            //OilPainting --- нормальный Sepia
            //HueModifier filter = new AForge.Imaging.Filters.HueModifier((int)(359 * mapping));
            int i = 3;

            pictureBox2.Image = iProcc.DoPreView(iProcc.ttt(i, mapping));
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

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           // iProcc.filters_correction.Corrections[i].param = mapping;
            int i = listView1.HitTest(e.Location).Item.Index;
            pictureBox2.Image = iProcc.DoWithUndo(iProcc.ttt(i));//new Filter_Command(iProcc.filters_correction.Filters[i])
           
        }
    }
}
