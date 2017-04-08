using Proj.ProcessImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projector
{
    class MainWindowViewModel
    {
        
        ImageProcessWin iProcc = new ImageProcessWin();
        private void Open_File(object sender, RoutedEventArgs e)
        {
            iProcc.LoadFromFile();
            //image.Source = iProcc.CurrentImageSource;
           // var tt = iProcc.GetImageFilters();


        }
    }
}
