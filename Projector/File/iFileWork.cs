using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.File
{
    interface iFileWork<T>
    {
        /// <summary>
        /// Load image from file
        /// </summary>
        void SaveToFile(T inputToSave);
        /// <summary>
        /// Save image to file
        /// </summary>
        T LoadFromFile();
    }
}
