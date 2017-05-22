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
    /// <summary>
    /// Implementation of Icommand Interface, represents Filter as Command...
    /// </summary>
    public class Filter_Command : ICommand<Bitmap>
    {
        private Bitmap _undoValue;
        private iFilter _filter;
        public Filter_Command(iFilter filter)
        {
            // _value = value;
            _filter = filter;
        }
        #region  ICommand
        public Bitmap Do(Bitmap input)
        {
            _undoValue = (Bitmap)input.Clone();
            return _filter.Apply(input);
        }
        public Bitmap Undo(Bitmap input)
        {
            return _undoValue;
        }
        public Bitmap DoPreView(Bitmap input)
        {
            return _filter.Apply(input);
        }
        #endregion
    }
   
   
}
