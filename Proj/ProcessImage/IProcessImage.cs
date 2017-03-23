using Proj.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.ProcessImage
{
    interface IProcessImage<T>
    {

        void LoadFromFile();
        void SaveToFile();
        T OriginalImage { get; }
        T CurrentImage { get; }
        T DoPreView(ICommand<T> cmd);
        T DoWithUndo(ICommand<T> cmd);
        T UnDo(T input);
        T Redo(T input);
    }
}
