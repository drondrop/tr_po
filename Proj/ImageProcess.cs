using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj
{
    interface ImageProcess
    {

        Bitmap LoadFromFile(string filePath);
        void SaveToFile(string filePath);
        Bitmap OriginalImage { get; }
        Bitmap CurrentImage { get; }

        void DoPreView();
        Bitmap DoWithUndo(ICommand<Bitmap> cmd, Bitmap input);
        Bitmap UnDo(Bitmap input);
        Bitmap Redo(Bitmap input);
    }
    public class ImageProcessWin : ImageProcess
    {
        private Bitmap _originalImage;
        private Bitmap _currentImage;
        private UndoRedoFactory<Bitmap> _imageUndoRedoFactory;

        public Bitmap LoadFromFile(string filePath)
        {
            return new Bitmap(_currentImage);
        }
        public void SaveToFile(string filePath)
        {

        }


        public void DoPreView() { }
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
