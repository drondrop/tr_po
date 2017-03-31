using Proj.Command;
using Proj.File;
using Proj.Filters;
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
   
    public abstract class ImageProcess<T>
    {
        protected T _originalImage;
        protected T _currentImage;
        protected UndoRedoKeper<T> _imageUndoRedoFactory;
        protected ImageProcess()
        {
            _imageUndoRedoFactory = new UndoRedoKeper<T>();
        }
    }
    public class ImageProcessWin :ImageProcess<Bitmap>, IProcessImage<Bitmap>
    {
        public Filter_factory filters_correction = new  Filter_factory();
        
        public void  LoadFromFile()
        {
            _imageUndoRedoFactory.Reset();
           _originalImage= FileWorkWinHelper.LoadFromFile();
           _currentImage = _originalImage;
            
        }
        public void SaveToFile()
        {
            FileWorkWinHelper.SaveToFile(_currentImage);
        }
#region  IProcessImage 

        public Bitmap DoPreView(ICommand<Bitmap> cmd) 
        {
            return _imageUndoRedoFactory.DoPreView(cmd, _currentImage);
        }
        public Bitmap DoWithUndo(ICommand<Bitmap> cmd)
        {
            _currentImage = _imageUndoRedoFactory.Do(cmd, _currentImage);
            return _currentImage;
        }
        public Bitmap UnDo(Bitmap input)
        {
            _currentImage=_imageUndoRedoFactory.Undo(_currentImage);
            return _currentImage;
        }
        public Bitmap Redo(Bitmap input)
        {
            _currentImage=_imageUndoRedoFactory.Redo(_currentImage);
            return _currentImage;
        }
        public Bitmap OriginalImage { get { return _originalImage; } }
        public Bitmap CurrentImage { get { return _currentImage; } }
#endregion
    }
}
