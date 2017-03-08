using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj
{
    interface ImageProcess <T>
    {

        T LoadFromFile(string filePath);
        void SaveToFile(string filePath);
        T OriginalImage { get; }
        T CurrentImage { get; }

        T DoPreView(ICommand<T> cmd);
        T DoWithUndo(ICommand<T> cmd);
        T UnDo(T input);
        T Redo(T input);
    }
    public class ImageProcessWin : ImageProcess<Bitmap>
    {
        private Bitmap _originalImage;
        private Bitmap _currentImage;
        private UndoRedoFactory<Bitmap> _imageUndoRedoFactory;

        public ImageProcessWin()
        {
            _imageUndoRedoFactory = new UndoRedoFactory<Bitmap>();
        }
        public Bitmap LoadFromFile(string filePath)
        {
            _imageUndoRedoFactory.Reset();

            _originalImage =(Bitmap) System.Drawing.Image.FromFile(filePath);
              _currentImage=_originalImage;
            return _originalImage;
        }
        public void SaveToFile(string filePath)
        {

        }


        public Bitmap DoPreView(ICommand<Bitmap> cmd) 
        {
            return _imageUndoRedoFactory.DoPreView(cmd, _currentImage);
        }
        public Bitmap DoWithUndo(ICommand<Bitmap> cmd)
        {
            _currentImage=_imageUndoRedoFactory.Do(cmd, _currentImage);
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
    }
}
