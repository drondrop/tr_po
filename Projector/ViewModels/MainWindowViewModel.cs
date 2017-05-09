using Proj.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Projector.ViewModels
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
        ObservableCollection<ListItem> m_lstFilters = new ObservableCollection<ListItem>();
        ObservableCollection<ListItem> m_lstCorrections = new ObservableCollection<ListItem>();
        private BitmapImage _ButtonImage;
        ImageProcessWin _iProcc;

        #region Constructors
        public ViewModel()
        {
            _iProcc = new ImageProcessWin();
           // ItemClickedCommand = new DelegateCommand<ItemClickEventArgs>(OnItemClicked);
        }
        public ViewModel(ImageProcessWin iProcc)
        {
            _iProcc = iProcc;
            var ttt = iProcc.GetImageFilters();
            var tttt = iProcc.PhoFilterNames;
            for (int i = 0; i < tttt.Count; i++)
            {
                m_lstFilters.Add(new ListItem() { ImageData = Bitmap2BitmapImage(ttt[i]), Title = tttt[i] });
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
            get { return m_lstFilters; }
            set
            {
                if (value != m_lstFilters)
                {
                    m_lstFilters = value;
                    InvokePropertyChanged("ProductList");

                }

            }
        }
        public ObservableCollection<ListItem> CorrectionList
        {
            get { return m_lstCorrections; }
            set
            {
                if (value != m_lstCorrections)
                {
                    m_lstCorrections = value;
                    InvokePropertyChanged("CorrectionList");

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
        #region OpenCommand
        private ICommand _OpenCommand;
        public ICommand OpenCommand
        {
            get
            {
               return _OpenCommand ?? (_OpenCommand = new RelayCommand(param => OpenCommandExecute(),param => OpenCommandCanExecute));
                
            }
        }
        private bool OpenCommandCanExecute
        {
            get { return true; }
        }
        private void OpenCommandExecute()
        {
            _iProcc.LoadFromFile();
            ButtonImage = Bitmap2BitmapImage(_iProcc.CurrentImage);

            var ImageFilters = _iProcc.GetImageFilters();
            var PhoFilterNames = _iProcc.PhoFilterNames;
            var ObservableListItemFilters = new ObservableCollection<ListItem>();
            for (int i = 0; i < PhoFilterNames.Count; i++)
            {
                ObservableListItemFilters.Add(new ListItem() { ImageData = Bitmap2BitmapImage(ImageFilters[i]), Title = PhoFilterNames[i] });
            }
            ProductList = ObservableListItemFilters;

            var ImageCorrection = _iProcc.GetImageCorrection();
            var CorrectionFilterNames = _iProcc.PhoCorrectionNames;
            var ObservableListItemCorrection = new ObservableCollection<ListItem>();
            for (int i = 0; i < CorrectionFilterNames.Count; i++)
            {
                ObservableListItemCorrection.Add(new ListItem() { ImageData = Bitmap2BitmapImage(ImageCorrection[i]), Title = CorrectionFilterNames[i] });
            }
            CorrectionList = ObservableListItemCorrection;


        }
        #endregion
        #region SaveCommand
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new RelayCommand(param => SaveCommandExecute(), param => SaveCommandCanExecute));

            }
        }
        private bool SaveCommandCanExecute
        {
            get { return true; }
        }
        private void SaveCommandExecute()
        {
            _iProcc.SaveToFile();
        }
        #endregion
        #region ReDoCommand
        private ICommand _ReDoCommand;
        public ICommand ReDoCommand
        {
            get
            {
                return _ReDoCommand ?? (_ReDoCommand = new RelayCommand(param => ReDoCommandExecute(), param => ReDoCommandCanExecute));

            }
        }
        private bool ReDoCommandCanExecute
        {
            get { return true; }
        }
        private void ReDoCommandExecute()
        {
            _iProcc.Redo(_iProcc.CurrentImage);
            ButtonImage = Bitmap2BitmapImage(_iProcc.CurrentImage);
        }
        #endregion
        #region UnDoCommand
        private ICommand _UnDoCommand;
        public ICommand EnDoCommand
        {
            get
            {
                return _UnDoCommand ?? (_UnDoCommand = new RelayCommand(param => UnDoCommandExecute(), param => UnDoCommandCanExecute));

            }
        }
        private bool UnDoCommandCanExecute
        {
            get { return true; }
        }
        private void UnDoCommandExecute()
        {
            _iProcc.UnDo(_iProcc.CurrentImage);
            ButtonImage = Bitmap2BitmapImage(_iProcc.CurrentImage);
        }
        #endregion
        #region FilterApplyCommand
        private ICommand _FilterApplyCommand;
        public ICommand FilterApplyCommand
        {
            get
            {
                return _FilterApplyCommand ?? (_FilterApplyCommand = new RelayCommand(param => FilterApplyCommandExecute(param), param => FilterApplyCommandCanExecute));

            }
        }
        private bool FilterApplyCommandCanExecute
        {
            get { return true; }
        }
        private void FilterApplyCommandExecute(object param)
        {
            var ttt = param as ListItem;
           _iProcc.DoWithUndo(_iProcc.CreateICommand(ttt.Title));
            ButtonImage = Bitmap2BitmapImage(_iProcc.CurrentImage);

        }
        #endregion


      
        #endregion
    }

    
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action<object> execute)
            : this(execute, (Predicate<object>)null)
        {
            this._execute = execute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this._execute = execute;
            this._canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (this._canExecute != null)
                return this._canExecute(parameter);
            else
                return true;
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }





    
}
