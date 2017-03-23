using AForge.Imaging.Filters;
using Proj.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Command
{
    public interface ICommand<T>
    {
        /// <summary>
        /// Do operation
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Result</returns>
        T Do(T input);
        T Undo(T input);
        T DoPreView(T input);
    }


   
   

}
