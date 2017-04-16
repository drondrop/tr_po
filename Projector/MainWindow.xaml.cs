
using Proj.Command;
using Projector.ViewModels;
using System;
using System.Windows;

namespace Projector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageProcessWin iProcc = new ImageProcessWin();
        ViewModel vmProduct = new ViewModel();

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = vmProduct;
            //
        }
        #region thisWillBe removed
        private void MenuItem_Open_File(object sender, RoutedEventArgs e)
        {
            iProcc.LoadFromFile();
           // image.Source = iProcc.CurrentImageSource;
            // var tt = iProcc.GetImageFilters();
            ViewModel vmProduct = new ViewModel(iProcc);
            //this.DataContext = vmProduct;

        }
        private void MenuEditUndo_Click(object sender, EventArgs e)
        {
            iProcc.UnDo(iProcc.CurrentImage);
            image.Source = iProcc.CurrentImageSource;

        }
        private void MenuEditRedo_Click(object sender, EventArgs e)
        {
            iProcc.Redo(iProcc.CurrentImage);
            image.Source = iProcc.CurrentImageSource;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion



    }
}
