using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Filters
{
    public interface ICFilter : iFilter
    {

        /// <summary>
        /// Apply filter on a image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Bitmap Apply(Bitmap image);
        double param { set; }
    }
    public interface iFilter
    {
        /// <summary>
        /// Apply filter on a image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Bitmap Apply(Bitmap image);
    }

}
