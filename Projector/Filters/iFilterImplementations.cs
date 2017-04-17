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
    public class Grayscale_filterBase 
    {
        Grayscale _filter;
        public Grayscale_filterBase(double cr = 0.2125, double cg = 0.7154, double cb = 0.0721)
        {
            _filter = new Grayscale(cr, cg, cb);
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }
    public class Grayscale_filter :Grayscale_filterBase, iPhoFilter
    {
        public Grayscale_filter()
            : base(0.2125, 0.7154, 0.0721)
        {
            
        }
    }
    public class YCbCr_filterBase 
    {
        YCbCrFiltering _filter;
        public YCbCr_filterBase(Range Cb, Range Cr, Range Y)
        {
            _filter = new YCbCrFiltering(Y, Cb, Cr);
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }
    public class YCbCr_filter : YCbCr_filterBase,iPhoFilter
    {
        
        public YCbCr_filter()
            : base(new Range(-0.5f, 0.5f), new Range(-0.5f, 0.5f), new Range(0, 0.9f))
        {
           
        }
    }
    public class ColorFiltering_filterBase 
    {
        ColorFiltering _filter;
        public ColorFiltering_filterBase(IntRange r, IntRange g, IntRange b)
        {
            _filter = new ColorFiltering(r, g, b);
        }
        public Bitmap Apply(Bitmap input)
        {
            return _filter.Apply(input);
        }

    }
    public class ColorFiltering_filter : ColorFiltering_filterBase,iPhoFilter
    {
        public ColorFiltering_filter()
            : base(new IntRange(10, 255), new IntRange(50, 255), new IntRange(0, 255))
        {
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
    #region masked
    public abstract class Masked_Helper
    {
        protected Bitmap _mask;
        protected Masked_Helper()
        {
            _mask = (Bitmap)Projector.Properties.Resources.main;
            _mask = Grayscale.CommonAlgorithms.BT709.Apply(_mask);
        }

    }
    public class Masked_filterBase1 : Masked_Helper
    {
        protected BrightnessCorrection _brightnessCorrection;
        public Masked_filterBase1(int bCorrection)
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
    public class Masked_filterBase2 : Masked_Helper
    {
        BrightnessCorrection _brightnessCorrection;
        public Masked_filterBase2(int bCorrection)
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
    public class Masked_filterBase3 : Masked_Helper 
    {
        BrightnessCorrection _brightnessCorrection;
        public Masked_filterBase3(int bCorrection)
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
    public class Masked_filterBase4 : Masked_Helper 
    {
        BrightnessCorrection _brightnessCorrection;
        public Masked_filterBase4(int bCorrection)
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
    public class Masked_filter1 : Masked_filterBase1, iPhoFilter
    {
       
        public Masked_filter1()
            : base(19)
        {
           
        }
       

    }
    public class Masked_filter2 : Masked_filterBase2, iPhoFilter
    {   
        public Masked_filter2()
            : base(150)
        {
            
        }
    }
    public class Masked_filter3 : Masked_filterBase3, iPhoFilter
    {
        
        public Masked_filter3()
            : base(0)
        {
            
        }
       

    }
    public class Masked_filter4 : Masked_filterBase4, iPhoFilter
    {   
        public Masked_filter4()
            : base(0)
        {
           
        }
    }
    #endregion
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
