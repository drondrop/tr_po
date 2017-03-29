using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Filters
{

    public class Saturation_Correction : iCFilter
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
    public class Brightness_Correction : iCFilter
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
    public class Contrast_Correction : iCFilter
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
    public class HueModifier_Correction : iCFilter
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

  

    public class Filter_factory
    {
        private List<iCFilter> _correctionFiltersCollection;
        private List<iFilter> _photoFiltersCollection;
        public Filter_factory()
        {
            _correctionFiltersCollection = new List<iCFilter>(){
               new Saturation_Correction(),
               new Brightness_Correction(),
               new Contrast_Correction(),
               new HueModifier_Correction()
            };
            _photoFiltersCollection = new List<iFilter>(){
                new Grayscale_filter(),
                new Grayscale_filter(0.2125,0.7154,0.0721),
                new YCbCr_filter(new Range( -0.5f, 0.5f ),new Range( -0.5f, 0.5f ),new Range( 0, 0.9f )),
                new ColorFiltering_filter(new IntRange(10,255),new IntRange(50,255),new IntRange(0,255)),
                new Masked_filter1(100),
                new Masked_filter2(150),
                new Masked_filter3(0),
                new Masked_filter4(0),
                new Invertion_filter()
            };
        }









        public List<iCFilter> Corrections
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


