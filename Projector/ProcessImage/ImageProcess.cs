using Proj.Command;
using Proj.File;
using Proj.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
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
        private Factory<iPhoFilter> _iPhoFilterFactory = new Factory<iPhoFilter>();
        private Factory<iParamFilter> _iParamFilterFactory = new Factory<iParamFilter>();
        public ImageProcessWin()
        {
            _iPhoFilterFactory.ScanForT(Assembly.GetExecutingAssembly());
            _iParamFilterFactory.ScanForT(Assembly.GetExecutingAssembly());
        }

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

        public List<string> PhoFilterNames { get { return _iPhoFilterFactory.getAllTypeNames; } }



        public List<Bitmap> GetImageFilters()
        {
            List<Bitmap> inmg = new List<Bitmap>();
          //  inmg.ImageSize = new System.Drawing.Size(64, 64);
            foreach (var t in _iPhoFilterFactory.getAllTypeNames)
            {
                // t.param=0.6;
                inmg.Add(_iPhoFilterFactory.Create(t).Apply(CurrentImage));
            }
            // ImageL
            return inmg;
        }
        ////
        /// ////////////////////////////
        /////
      
        public Filter_Command CreateICommand(string FilterName)
        {
            return new Filter_Command(_iPhoFilterFactory.Create(FilterName));
        }
        public Filter_Command CreateICommand(string FilterName, double param)
        {
            var _filtr = _iParamFilterFactory.Create(FilterName);
            _filtr.param = param;
            return new Filter_Command(_filtr);
        }
        
        /// ////////////
       

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
    public class Factory<T> where T : class
    {
        private Dictionary<string, Type> _foundTemplTypes =
            new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        public List<string> getAllTypeNames
        {
            get
            {
                var Keys = _foundTemplTypes.Keys.ToList<string>();
                return Keys;
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="assemblies"></param>
        public void ScanForT(params Assembly[] assemblies) 
        {
            var typeT = typeof(T);
            
            foreach (var assembly in assemblies)
            {

                foreach (var type in assembly.GetTypes())
                {
                    if (!typeT.IsAssignableFrom(type)
                        
                        || type.IsAbstract
                        || type.IsInterface)
                        continue;
                    _foundTemplTypes.Add(type.Name, type);
                }
            }

        }

        /// <summary>
        /// Create some food!
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Create(string name)
        {
            Type type;
            if (!_foundTemplTypes.TryGetValue(name, out type))
                throw new ArgumentException("Failed to find T named '" + name + "'.");

            return (T)Activator.CreateInstance(type);
        }
    }
}
