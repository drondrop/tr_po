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
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

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
    public class ImageProcessWin : ImageProcess<Bitmap>, IProcessImage<Bitmap>
    {
        public Filter_factory filters_correction = new Filter_factory();

        public void LoadFromFile()
        {
            _imageUndoRedoFactory.Reset();
            _originalImage = FileWorkWinHelper.LoadFromFile();
            _currentImage = _originalImage;

        }
        public void SaveToFile()
        {
            FileWorkWinHelper.SaveToFile(_currentImage);
        }
        public List<string> filterNames { get { return filters_correction.Filters.Select(x => x.ToString()).ToList<string>(); } }
        public List<Bitmap> GetImageFilters()
        {
            List<Bitmap> inmg = new List<Bitmap>();
          //  inmg.ImageSize = new System.Drawing.Size(64, 64);
            foreach (var t in filters_correction.Filters)
            {
                // t.param=0.6;
                inmg.Add(t.Apply(CurrentImage));
            }
            // ImageL
            return inmg;
        }

        public Filter_Command ttt(int i)
        {
            return new Filter_Command(filters_correction.Filters[i]);
        }
        public Filter_Command ttt(int i, double param)
        {
            filters_correction.Corrections[i].param = param;
            return new Filter_Command(filters_correction.Corrections[i]);
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
            _currentImage = _imageUndoRedoFactory.Undo(_currentImage);
            return _currentImage;
        }
        public Bitmap Redo(Bitmap input)
        {
            _currentImage = _imageUndoRedoFactory.Redo(_currentImage);
            return _currentImage;
        }
        public Bitmap OriginalImage { get { return _originalImage; } }
        public Bitmap CurrentImage { get { return _currentImage; } }

        #endregion
        public BitmapSource CurrentImageSource
        {
            get
            {
                return conv(_currentImage);
            }
        }
        private static BitmapSource conv(Bitmap _Image)
        {
            return
              System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                 _Image.GetHbitmap(),
                  IntPtr.Zero,
                  Int32Rect.Empty,
                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }

    }
}
