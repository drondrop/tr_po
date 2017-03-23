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

}
