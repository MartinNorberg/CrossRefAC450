namespace AC450Communication
{
    using Microsoft.WindowsAPICodePack.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Input;

    public class ViewModel : INotifyPropertyChanged
    {
        private string pcPath = "Path to PCDATA folder";
        private string dbPath = "Path to DBDATA folder";
        
        public ViewModel()
        {
            this.BrowsePcPath = new RelayCommand(_ =>
            {
                this.PcPath = this.GetPath();
            });

            this.BrowseDbPath = new RelayCommand(_ =>
            {
                this.DbPath = this.GetPath();
            });
        }

        public ICommand BrowsePcPath { get; }

        public ICommand BrowseDbPath { get; }

        public ICommand GenerateData { get; }

        public string PcPath
        {
            get => this.pcPath;
            set
            {
                if (value == this.pcPath)
                {
                    return;
                }

                this.pcPath = value;
                this.OnPropertyChanged();
            }
        }

        public string DbPath
        {
            get => this.dbPath;
            set
            {
                if (value == this.dbPath)
                {
                    return;
                }

                this.dbPath = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string GetPath()
        {
            var path = string.Empty;
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
            })
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    path = dialog.FileName;
                }

                return path;
            }
        }

        private bool TryGenerateData()
        {
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
