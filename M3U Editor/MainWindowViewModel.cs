using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace M3U_Editor
{
    class MainWindowViewModel : ViewModelBase
    {
        private string _fileName;
        private M3uFile _m3uFile;

        public RelayCommand LoadCommand { get; }
        public RelayCommand SaveCommand { get; }

        public M3uFile M3uFile
        {
            get => _m3uFile;
            private set
            {
                _m3uFile = value;
                RaisePropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            LoadCommand = new RelayCommand(LoadAction, LoadCanAction);
            SaveCommand = new RelayCommand(SaveAction, SaveCanAction);
        }

        private void LoadAction()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "M3U files|*.m3u";
            dialog.Multiselect = false;

            var result = dialog.ShowDialog();
            if (result.HasValue == false || result.Value == false) return;
            _fileName = dialog.FileName;
            if (File.Exists(_fileName) == false) return;

            M3uFile = new M3uFile(_fileName);
        }

        private bool LoadCanAction()
        {
            return true;
        }

        private void SaveAction()
        {
            var result = MessageBox.Show($"Are you sure you want to overwrite the original file ?{Environment.NewLine}{_fileName}", string.Empty, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel) return;

            File.WriteAllLines(_fileName, M3uFile.Lines.SelectMany(x => x.Lines));
        }

        private bool SaveCanAction()
        {
            return M3uFile != null;
        }
    }
}
