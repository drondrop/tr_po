using Proj.ProcessImage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageProcessWin iProcc = new ImageProcessWin();

        public MainWindow()
        {
            
            InitializeComponent();
            //
        }
        private void MenuItem_Open_File(object sender, RoutedEventArgs e)
        {
            iProcc.LoadFromFile();
            image.Source = iProcc.CurrentImageSource;
            // var tt = iProcc.GetImageFilters();
            ViewModel vmProduct = new ViewModel(iProcc);
            this.DataContext = vmProduct;

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

       
        public class ListItem
        {
            private string _Title;
            public string Title
            {
                get { return this._Title; }
                set { this._Title = value; }
            }

            private BitmapImage _ImageData;
            public BitmapImage ImageData
            {
                get { return this._ImageData; }
                set { this._ImageData = value; }
            }

        }
        class ViewModel
        {
            ObservableCollection<ListItem> m_lstProducts = new ObservableCollection<ListItem>();
            ImageProcessWin _iProcc ;
            public ViewModel(ImageProcessWin iProcc)
            {
                _iProcc = iProcc;
                var ttt= iProcc.GetImageFilters();
                var tttt = iProcc.filterNames;
                for(int i =0;i<tttt.Count;i++)
                {
                    m_lstProducts.Add(new ListItem() { ImageData = Bitmap2BitmapImage(ttt[i]), Title = tttt[i] });//new BitmapImage(new Uri("D:\\study\\master\\2 semestr\\tr_po\\project\\tr_po-interface\\Projector\\1.jpg"))
                }
                
            }
            private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
            {
                
                    using (MemoryStream memory = new MemoryStream())
                    {
                        bitmap.Save(memory, ImageFormat.Png);
                        memory.Position = 0;
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        return bitmapImage;
                    }
                    
            }
            public ObservableCollection<ListItem> ProductList
            {
                get { return m_lstProducts; }
            }
        }

       

    }
}
