using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Filters
{
    public interface ICFilter
    {
        Bitmap Apply(Bitmap image);
        double param { set; }
    }

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
                _param =(int) (value * 255*2) - 255;
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
                _param = (int)(value * 127*2) - 127;
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

    public interface IPhFilter
    {
    }
    public class Filter_factory
    {
        public static List<ICFilter> getFilters()
        {
            List<ICFilter> fList = new List<ICFilter>(){
               new Saturation_Correction(),
               new Brightness_Correction(),
               new Contrast_Correction(),
               new HueModifier_Correction()
            };
            return fList;
        }
        
    }

}
