using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj
{
    interface ImageProcess
    {

        public Bitmap LoadFromFile(string filePath);
        public void SaveToFile(string filePath);
        public Bitmap OriginalImage { get; }
        public Bitmap CurrentImage { get; }

        
    }
}
