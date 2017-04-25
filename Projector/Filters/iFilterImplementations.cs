using AForge;
using AForge.Imaging.Filters;
using System.Drawing;

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
    public class Grayscale_filter : Grayscale_filterBase
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
    public class YCbCr_filter : YCbCr_filterBase
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
    public class ColorFiltering_filter : ColorFiltering_filterBase
    {
        public ColorFiltering_filter()
            : base(new IntRange(10, 255), new IntRange(50, 255), new IntRange(0, 255))
        {
        }
    }

    public class Invertion_filter
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

        protected Bitmap Mask
        {
            get { return _mask; }
        }

        private Bitmap _mask;
        protected Masked_Helper()
        {
            _mask = (Bitmap)Projector.Properties.Resources.main;
            _mask = Grayscale.CommonAlgorithms.BT709.Apply(_mask);

            // Mask = _brightnessCorrection.Apply(Mask);
        }
        protected static Bitmap BrightnessCorrection(Bitmap input, int CorrectionValue)
        {
            var Correction = new BrightnessCorrection(CorrectionValue);
            return Correction.Apply(input);

        }
        public static Bitmap Invert(Bitmap input)
        {
            var Filter = new Invert();
            return Filter.Apply(input);
        }


    }
    public class Masked_filterBase1 : Masked_Helper
    {
        int _bCorrection;
        public Masked_filterBase1(int bCorrection)
            : base() { _bCorrection = bCorrection; }

        public Bitmap Apply(Bitmap input)
        {

            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(Mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);

            resMask = Masked_Helper.BrightnessCorrection(resMask, _bCorrection);
            resMask = Masked_Helper.Invert(resMask);
            //resMask = invertFilter.Apply(resMask);
            Subtract subtractFilter = new Subtract(resMask);
            return subtractFilter.Apply(input);
        }
    }
    public class Masked_filterBase2 : Masked_Helper
    {
        int _bCorrection;
        public Masked_filterBase2(int bCorrection)
            : base() { _bCorrection = bCorrection; }
        public Bitmap Apply(Bitmap input)
        {

            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(Mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);

            resMask = Masked_Helper.BrightnessCorrection(resMask, _bCorrection);
            resMask = Masked_Helper.Invert(resMask);

            Add subtractFilter = new Add(resMask);
            return subtractFilter.Apply(input);
        }

    }
    public class Masked_filterBase3 : Masked_Helper
    {
        int _bCorrection;
        public Masked_filterBase3(int bCorrection)
            : base() { _bCorrection = bCorrection; }
        public Bitmap Apply(Bitmap input)
        {
            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(Mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);

            resMask = Masked_Helper.BrightnessCorrection(resMask, _bCorrection);


            Subtract subtractFilter = new Subtract(resMask);
            return subtractFilter.Apply(input);
        }

    }
    public class Masked_filterBase4 : Masked_Helper
    {
        int _bCorrection;
        public Masked_filterBase4(int bCorrection)
            : base() { _bCorrection = bCorrection; }
        public Bitmap Apply(Bitmap input)
        {
            var resize = new Resize_filter(input.Width, input.Height);
            var resMask = resize.Apply(Mask);
            GrayscaleToRGB grayscaleToRGBFilter = new GrayscaleToRGB();
            resMask = grayscaleToRGBFilter.Apply(resMask);

            resMask = Masked_Helper.BrightnessCorrection(resMask, _bCorrection);

            Add subtractFilter = new Add(resMask);
            return subtractFilter.Apply(input);
        }

    }
    public class Masked_filter1 : Masked_filterBase1
    {

        public Masked_filter1()
            : base(150) { }



    }
    public class Masked_filter2 : Masked_filterBase2
    {
        public Masked_filter2()
            : base(150) { }

    }
    public class Masked_filter3 : Masked_filterBase3
    {

        public Masked_filter3()
            : base(0) { }


    }
    public class Masked_filter4 : Masked_filterBase4
    {
        public Masked_filter4()
            : base(0)
        { }
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












































































    #region filter for alex
    /// <summary>
    /// amaro - виньетка (затемнение краев), увеличение яркости/
    /// увеличение яркости только в центре,
    /// маленькое уменьшение насыщенности
    /// </summary>
    public class Aamaro : Masked_Helper, iPhoFilter
    {
        public Aamaro() { }
        public Bitmap Apply(Bitmap input)
        {
            return effect_Helper.Vignette(input);//subtractFilter.Apply(input);
        }

    }
    //rise - виньетка, смещение баланса белого в сторону желтого, уменьшение контраста	
    public class rise : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.Vignette(image);
            image = ImageCorrectionHelper.CorrectContrast(image, -10);
            image = effect_Helper.PaintMask(image, Color.FromArgb(30, 255, 255, 0));
            return image;
        }
    }

    //hudson - сильная виньетка, увеличение контраста и яркости, чуть розовый оттенок
    public class hudson : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.Vignette(image);
            image = ImageCorrectionHelper.CorrectContrast(image, 10);
            image = ImageCorrectionHelper.CorrectBrightness(image, 10);
            image = effect_Helper.PaintMask(image, Color.FromArgb(20, 255, 105, 180));
            return image;
        }
    }
    //x pro II - сильная виньетка, увеличение контраста и насыщенности
    public class x_pro_II : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.Vignette(image);
            image = ImageCorrectionHelper.CorrectContrast(image, 15);
            image = ImageCorrectionHelper.CorrectSaturation(image, 0.1f);
            return image;
        }
    }

    //sierra - уменьшение насыщенности, смещение баланса белого в сторону желтого
    public class sierra : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = ImageCorrectionHelper.CorrectSaturation(image, -0.1f);
            image = effect_Helper.PaintMask(image, Color.FromArgb(30, 255, 255, 0));
            return image;
        }
    }


    //lo-fi - виньетка, увеличение яркости и уменьшение контраста
    public class lo_fi : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.Vignette(image);
            image = ImageCorrectionHelper.CorrectContrast(image, -10);
            image = ImageCorrectionHelper.CorrectBrightness(image, 10);
            return image;
        }
    }
    //earlybird - виньетка, уменьшение насыщенности, желтый оттенок
    public class earlybird : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.Vignette(image);
            image = ImageCorrectionHelper.CorrectSaturation(image, -0.1f);
            image = effect_Helper.PaintMask(image, Color.FromArgb(30, 255, 255, 0));
            return image;
        }
    }
    //sutro - виньетка, уменьшение яркости, насыщенности и контраста, чуть розовый оттенок
    public class sutro : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.PaintMask(image, Color.FromArgb(20, 255, 105, 180));
            image = effect_Helper.Vignette(image);
            image = ImageCorrectionHelper.CorrectContrast(image, -10);
            image = ImageCorrectionHelper.CorrectBrightness(image, -10);
            image = ImageCorrectionHelper.CorrectSaturation(image, -0.1f);
            return image;
        }
    }
    //toaster - розовый оттенок, уменьшение контраста
    public class toaster : iPhoFilter
    {

        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.PaintMask(image, Color.FromArgb(30, 255, 105, 180));
            image = ImageCorrectionHelper.CorrectContrast(image, -10);
            return image;
        }


    }
    //brannan - уменьшение насышенности и контраста
    public class brannan : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = ImageCorrectionHelper.CorrectContrast(image, -10);
            image = ImageCorrectionHelper.CorrectSaturation(image, -0.2f);
            return image;
        }
    }

    //inkwell - насыщенность в ноль
    public class inkwell : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = ImageCorrectionHelper.CorrectSaturation(image, -1f);
            return image;
        }
    }

    //walden - увеличение яркости, голубой оттенок
    public class walden : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = ImageCorrectionHelper.CorrectBrightness(image, 15);
            return effect_Helper.PaintMask(image, Color.FromArgb(30, 0, 0, 255));
            return image;
        }
    }

    //hefe - желтый оттенок, виньетка, чуть увеличение яркости
    public class hefe : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.Vignette(image);
            image = ImageCorrectionHelper.CorrectBrightness(image, 5);
            image = effect_Helper.PaintMask(image, Color.FromArgb(30, 255, 255, 0));
            return image;
        }
    }

    //valensia - увеличение яркости, уменьшение контраста, уменьшение насыщенности
    public class valensia : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = ImageCorrectionHelper.CorrectContrast(image, -10);
            image = ImageCorrectionHelper.CorrectBrightness(image, 10);
            image = ImageCorrectionHelper.CorrectSaturation(image, -0.3f);
            return image;
        }
    }


    //nashville - голубой оттенок, увеличение яркости+насыщенности
    public class nashville : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = ImageCorrectionHelper.CorrectBrightness(image, 20);
            return effect_Helper.PaintMask(image, Color.FromArgb(30, 0, 0, 255));
            image = ImageCorrectionHelper.CorrectSaturation(image, 0.5f);
        }
    }

    //1977 - розовый оттенок, чуть уменьшение насыщенности

    public class year : iPhoFilter
    {

        public Bitmap Apply(Bitmap image)
        {
            image = effect_Helper.PaintMask(image, Color.FromArgb(30, 255, 105, 180));
            image = ImageCorrectionHelper.CorrectSaturation(image, -0.1f);
            return image;
        }
    }


    //kelvin - увеличение яркости, контраста, желтый оттенок
    public class kelvin : iPhoFilter
    {
        public Bitmap Apply(Bitmap image)
        {
            image = ImageCorrectionHelper.CorrectContrast(image, 10);
            image = ImageCorrectionHelper.CorrectBrightness(image, 10);
            return effect_Helper.PaintMask(image, Color.FromArgb(30, 255, 255, 0)); ;
        }
    }

    #endregion
}
