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

    public class Saturation_Correction : ICFilter
    {
        private SaturationCorrection _filter;
        private float _param;
        public Saturation_Correction()
        {
            _filter = new SaturationCorrection();
        }
        public double param
        {
            set
            {
                _param = (float)(value * 2) - 1;
            }
        }
        public Bitmap Apply(Bitmap input)
        {
            _filter.AdjustValue = _param;
            return _filter.Apply(input);
        }
    }
    public class Brightness_Correction : ICFilter
    {
        private BrightnessCorrection _filter;
        private int _param;
        public Brightness_Correction()
        {
            _filter = new BrightnessCorrection();
        }
        public double param
        {
            set
            {
                _param = (int)(value * 255 * 2) - 255;
            }
        }
        public Bitmap Apply(Bitmap input)
        {
            _filter.AdjustValue = _param;
            return _filter.Apply(input);
        }
    }
    public class Contrast_Correction : ICFilter
    {
        private ContrastCorrection _filter;
        private int _param;
        public Contrast_Correction()
        {
            _filter = new ContrastCorrection();
        }
        public double param
        {
            set
            {
                _param = (int)(value * 127 * 2) - 127;
            }
        }
        public Bitmap Apply(Bitmap input)
        {
            _filter.Factor = _param;
            return _filter.Apply(input);
        }
    }
    public class HueModifier_Correction : ICFilter
    {
        private HueModifier _filter;
        private int _param;
        public HueModifier_Correction()
        {
            _filter = new HueModifier();
        }
        public double param
        {
            set
            {
                _param = (int)(value * 359);
            }
        }
        public Bitmap Apply(Bitmap input)
        {
            _filter.Hue = _param;
            return _filter.Apply(input);
        }
    }




    public class Grayscale_filter : iFilter
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
    public class YCbCr_filter : iFilter
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
    public class ColorFiltering_filter : iFilter
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
    


    public class Filter_factory
    {
        private List<ICFilter> _correctionFiltersCollection;
        private List<iFilter> _photoFiltersCollection;
        public Filter_factory()
        {
            _correctionFiltersCollection = new List<ICFilter>(){
               new Saturation_Correction(),
               new Brightness_Correction(),
               new Contrast_Correction(),
               new HueModifier_Correction()
            };
            _photoFiltersCollection = new List<iFilter>(){
                new Grayscale_filter(),
                new Grayscale_filter(0.2125,0.7154,0.0721),
                new YCbCr_filter(new Range( -0.5f, 0.5f ),new Range( -0.5f, 0.5f ),new Range( 0, 0.9f )),
                new ColorFiltering_filter(new IntRange(10,255),new IntRange(50,255),new IntRange(0,255))

            };
        }
        public List<ICFilter> Corrections
        {
            get
            {
                return _correctionFiltersCollection;
            }
        }
        public List<iFilter> Filters
        {
            get
            {
                return _photoFiltersCollection;
            }
        }
    }


}


