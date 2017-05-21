
using Proj.Command;
using Projector.ViewModel;
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
       MainViewModel vmProduct = new MainViewModel();

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = vmProduct;
            //
        }
       

        private void slider2_DragOver(object sender, DragEventArgs e)
        {

        }



    }
}
