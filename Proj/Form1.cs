﻿using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Proj.Command;
using Proj.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Proj
{
    public partial class Form1 : Form
    {
        ImageProcessWin iProcc = new ImageProcessWin();
        List<ICFilter> filters_correction = Filter_factory.getFilters();
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
            ImageList inmg = new ImageList();
            inmg.ImageSize = new Size(64, 64);
            foreach(var t in filters_correction)
            {
                t.param=0.6;
               inmg.Images.Add( t.Apply(iProcc.CurrentImage));
            }
            listView1.LargeImageList = inmg;
            int tt=0;
            foreach (var t in filters_correction)
            {
                listView1.Items.Add(t.ToString(), tt);
                    tt++;
            }
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
