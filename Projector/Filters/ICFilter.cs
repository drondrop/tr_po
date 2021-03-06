﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Filters
{
    public interface iParamFilter : iFilter
    {
        /// <summary>
        /// Value of filter intenssivety
        /// </summary>
        double param { set; }
    }
    public interface iPhoFilter : iFilter
    {
        /// <summary>
        /// Apply filter on a image
        /// </summary>
        /// <param name="image"></param>
        /// <returns>New image after changes</returns>
    }
    public interface iFilter
    {
        /// <summary>
        /// Apply filter on a image
        /// </summary>
        /// <param name="image"></param>
        /// <returns>New image after changes</returns>
        Bitmap Apply(Bitmap image);
    }

}
