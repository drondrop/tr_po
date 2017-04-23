using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Filters
{
    /// <summary>
    /// ImageCorrectionHelper - Has static methods for image correction
    /// </summary>
    class ImageCorrectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">original input</param>
        /// <param name="param">[-1,1] float value</param>
        /// <returns>output image</returns>
        public static Bitmap CorrectSaturation(Bitmap image, float param)
        {
            SaturationCorrection filter = new SaturationCorrection(param);
            return filter.Apply(image);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">original input</param>
        /// <param name="param">[-255,255] int value</param>
        /// <returns>output image</returns>
        public static Bitmap CorrectBrightness(Bitmap image, int param)
        {
            BrightnessCorrection filter = new BrightnessCorrection(param);
            return filter.Apply(image);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">original input</param>
        /// <param name="param">[-127,127] int value</param>
        /// <returns>output image</returns>
        public static Bitmap CorrectContrast(Bitmap image, int param)
        {
            ContrastCorrection filter = new ContrastCorrection(param);
            return filter.Apply(image);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">original input</param>
        /// <param name="param">[0,360] int value </param>
        /// <returns>output image</returns>
        public static Bitmap CorrectHue(Bitmap image, int param)
        {
            HueModifier filter = new HueModifier(param);
            return filter.Apply(image);
        }
    }
}
