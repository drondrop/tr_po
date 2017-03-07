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

        T DoPreView(ICommand<T> cmd, T input);
        T DoWithUndo(ICommand<T> cmd, T input);
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
            return new Bitmap(_currentImage);
        }
        public void SaveToFile(string filePath)
        {

        }


        public Bitmap DoPreView(ICommand<Bitmap> cmd, Bitmap input) 
        {
            return _imageUndoRedoFactory.DoPreView(cmd, input);
        }
        public Bitmap DoWithUndo(ICommand<Bitmap> cmd, Bitmap input)
        {
            return _imageUndoRedoFactory.Do(cmd, input);
        }
        public Bitmap UnDo(Bitmap input)
        {
            return _imageUndoRedoFactory.Undo(input);
        }
        public Bitmap Redo(Bitmap input)
        {
            return _imageUndoRedoFactory.Redo(input);
        }

        public Bitmap OriginalImage { get { return _originalImage; } }
        public Bitmap CurrentImage { get { return _currentImage; } }
    }
}
