using Proj.ProcessImage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace Projector
{
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


    public class ViewModel : INotifyPropertyChanged
    {
        ObservableCollection<ListItem> m_lstProducts = new ObservableCollection<ListItem>();
        private BitmapImage _ButtonImage;
        ImageProcessWin _iProcc;

        #region Constructors
        public ViewModel()
        {
            _iProcc = new ImageProcessWin();
            _canExecute = true;
        }
        public ViewModel(ImageProcessWin iProcc)
        {
            _iProcc = iProcc;
            var ttt = iProcc.GetImageFilters();
            var tttt = iProcc.PhoFilterNames;
            for (int i = 0; i < tttt.Count; i++)
            {
                m_lstProducts.Add(new ListItem() { ImageData = Bitmap2BitmapImage(ttt[i]), Title = tttt[i] });
            }

        }
        #endregion
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

        #region Property
        public ObservableCollection<ListItem> ProductList
        {
            get { return m_lstProducts; }
            set
            {
                if (value != m_lstProducts)
                {
                    m_lstProducts = value;
                    InvokePropertyChanged("ProductList");

                }

            }
        }
       
        public BitmapImage ButtonImage
        {
            get { return _ButtonImage; }
            set
            {
                if (value != _ButtonImage)
                {
                    _ButtonImage = value;
                    InvokePropertyChanged("ButtonImage");

                }

            }
        }
        #endregion

        #region INotifyPropertyChanged
        private void InvokePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region ICommandExecution
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), _canExecute));
            }
        }
        private bool _canExecute;
        public void MyAction()
        {
            _iProcc.LoadFromFile();
            ButtonImage = Bitmap2BitmapImage(_iProcc.CurrentImage);

            var ttt = _iProcc.GetImageFilters();
            var tttt = _iProcc.PhoFilterNames;
            var ttttt = new ObservableCollection<ListItem>();
            for (int i = 0; i < tttt.Count; i++)
            {
                ttttt.Add(new ListItem() { ImageData = Bitmap2BitmapImage(ttt[i]), Title = tttt[i] });
            }
            ProductList = ttttt;
        }
        #endregion
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
