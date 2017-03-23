using Proj.Command;
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
   
    public class ImageProcessWin : IProcessImage<Bitmap>
    {
        private Bitmap _originalImage;
        private Bitmap _currentImage;
        private UndoRedoFactory<Bitmap> _imageUndoRedoFactory;
        public Filter_factory filters_correction = new  Filter_factory();
        public ImageProcessWin()
        {
            _imageUndoRedoFactory = new UndoRedoFactory<Bitmap>();
        }
        public void  LoadFromFile()
        {
            _imageUndoRedoFactory.Reset();
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "";
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                string sep = string.Empty;
                foreach (var c in codecs)
                {
                    string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                    open.Filter = String.Format("{0}{1}{2} ({3})|{3}", open.Filter, sep, codecName, c.FilenameExtension);
                    sep = "|";
                }
                open.Filter = String.Format("{0}{1}{2} ({3})|{3}", open.Filter, sep, "All Files", "*.*");
                open.DefaultExt = ".png"; // Default file extension 
                // Show open file dialog box 
                if (open.ShowDialog() == DialogResult.OK)
                {
                    _originalImage = (Bitmap)System.Drawing.Image.FromFile(open.FileName);
                    _currentImage = _originalImage;
                }
            }
        }
        public void SaveToFile()
        {
            using (SaveFileDialog save = new SaveFileDialog())
            {           
                save.Filter = "";
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                string sep = string.Empty;
                foreach (var c in codecs)
                {
                    string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                    save.Filter = String.Format("{0}{1}{2} ({3})|{3}", save.Filter, sep, codecName, c.FilenameExtension);
                    sep = "|";
                }
                save.Filter = String.Format("{0}{1}{2} ({3})|{3}", save.Filter, sep, "All Files", "*.*");
                save.DefaultExt = ".png"; // Default file extension 
                if (save.ShowDialog() == DialogResult.OK)
                {
                    _originalImage.Save(save.FileName);
                }
            }
        }


        public Bitmap DoPreView(ICommand<Bitmap> cmd) 
        {
            return _imageUndoRedoFactory.DoPreView(cmd, _currentImage);
        }
        public Bitmap DoWithUndo(ICommand<Bitmap> cmd)
        {
            _currentImage = _imageUndoRedoFactory.Do(cmd, _originalImage);
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
