using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proj.File
{
    public class FileWorkWin : iFileWork<Bitmap>
    {
        public void SaveToFile(Bitmap inputToSave)
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
                    inputToSave.Save(save.FileName);
                }
            }
        }

        public Bitmap LoadFromFile()
        {
            Bitmap ImgToReturn = null;
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
                    ImgToReturn = (Bitmap)System.Drawing.Image.FromFile(open.FileName);
                }
            }
            return ImgToReturn;
        }

    }
}
