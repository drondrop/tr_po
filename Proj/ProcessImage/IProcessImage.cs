using Proj.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proj.ProcessImage
{
    interface IProcessImage<T>
    {
       
        /// <summary>
        /// Original image without changes
        /// </summary>
        T OriginalImage { get; }
        /// <summary>
        /// Image with changes
        /// </summary>
        T CurrentImage { get; }
        /// <summary>
        /// Do command without apply any changes
        /// </summary>
        /// <param name="cmd">Command to do</param>
        /// <returns>Image</returns>
        T DoPreView(ICommand<T> cmd);
        /// <summary>
        ///  Do command without apply any changes
        /// </summary>
        /// <param name="cmd">Command to do</param>
        /// <returns>Image</returns>
        T DoWithUndo(ICommand<T> cmd);
        /// <summary>
        /// Cancel command 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Image</returns>
        T UnDo(T input);
        /// <summary>
        /// Cancel  Undo command 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Image</returns>
        T Redo(T input);
    }
   
   


}
