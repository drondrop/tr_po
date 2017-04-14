using AForge;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Filters
{
    public class Grayscale_filter : iPhoFilter
    {
        Grayscale _filter;
        public Grayscale_filter(double cr = 0.2125, double cg = 0.7154, double cb = 0.0721)
        {
            _filter = new Grayscale(cr, cg, cb);
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }
    public class YCbCr_filter : iPhoFilter
    {
        YCbCrFiltering _filter;
        public YCbCr_filter(Range Cb, Range Cr, Range Y)
        {
            _filter = new YCbCrFiltering(Y, Cb, Cr);
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }
    public class ColorFiltering_filter : iPhoFilter
    {
        ColorFiltering _filter;
        public ColorFiltering_filter(IntRange r, IntRange g, IntRange b)
        {
            _filter = new ColorFiltering(r, g, b);
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }

    public class Invertion_filter : iPhoFilter
    {
        Invert _filter;
        public Invertion_filter()
        {
            _filter = new Invert();
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }
    public class Masked_Helper
    {
        protected Bitmap _mask;
        protected Masked_Helper()
        {
            _mask = (Bitmap)Bitmap.FromFile("C:\\Users\\Andrew\\Desktop\\imaje\\main.png");
            _mask = Grayscale.CommonAlgorithms.BT709.Apply(_mask);
        }

    }
    public class Masked_filter1 : Masked_Helper
    {
        protected BrightnessCorrection _brightnessCorrection;
        public Masked_filter1(int bCorrection)
            : base()
        {
            _brightnessCorrection = new BrightnessCorrection(bCorrection);
        }
        public Bitmap Apply(Bitmap input)
        {

            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(_mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);


            resMask = _brightnessCorrection.Apply(resMask);

            Invert invertFilter = new Invert();
            resMask = invertFilter.Apply(resMask);

            Subtract subtractFilter = new Subtract(resMask);
            return subtractFilter.Apply(input);
        }

    }
    public class Masked_filter10 : Masked_filter1, iPhoFilter
    {
       
        public Masked_filter10()
            : base(19)
        {
           
        }
       

    }
    public class Masked_filter2 : Masked_Helper, iPhoFilter
    {
        BrightnessCorrection _brightnessCorrection;
        public Masked_filter2(int bCorrection)
            : base()
        {
            _brightnessCorrection = new BrightnessCorrection(bCorrection);
        }
        public Bitmap Apply(Bitmap input)
        {

            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(_mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);


            resMask = _brightnessCorrection.Apply(resMask);

            Invert invertFilter = new Invert();
            resMask = invertFilter.Apply(resMask);

            Add subtractFilter = new Add(resMask);
            return subtractFilter.Apply(input);
        }

    }
    public class Masked_filter3 : Masked_Helper, iPhoFilter
    {
        BrightnessCorrection _brightnessCorrection;
        public Masked_filter3(int bCorrection)
            : base()
        {
            _brightnessCorrection = new BrightnessCorrection(bCorrection);
        }
        public Bitmap Apply(Bitmap input)
        {
            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(_mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);
            resMask = _brightnessCorrection.Apply(resMask);
            Subtract subtractFilter = new Subtract(resMask);
            return subtractFilter.Apply(input);
        }

    }
    public class Masked_filter4 : Masked_Helper, iPhoFilter
    {
        BrightnessCorrection _brightnessCorrection;
        public Masked_filter4(int bCorrection)
            : base()
        {
            _brightnessCorrection = new BrightnessCorrection(bCorrection);
        }
        public Bitmap Apply(Bitmap input)
        {
            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(_mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);
            resMask = _brightnessCorrection.Apply(resMask);
            Add subtractFilter = new Add(resMask);
            return subtractFilter.Apply(input);
        }

    }
    public class Resize_filter 
    {
        ResizeBicubic _filter;
        public Resize_filter(int W, int H)
        {
            _filter = new ResizeBicubic(W, H);
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }

}
