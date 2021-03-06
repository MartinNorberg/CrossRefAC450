﻿namespace AC450Communication
{
    using Microsoft.WindowsAPICodePack.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Input;

    public class ViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<PcData> pcElemets = new ObservableCollection<PcData>();
        private readonly ObservableCollection<DbData> dbElemets = new ObservableCollection<DbData>();
        private string pcPath = "Path to PCDATA folder";
        private string dbPath = "Path to DBDATA folder";

        public ViewModel()
        {
            this.PcElements = new ReadOnlyObservableCollection<PcData>(this.pcElemets);

            this.DbElements = new ReadOnlyObservableCollection<DbData>(this.dbElemets);

            this.BrowsePcPath = new RelayCommand(_ =>
            {
                this.PcPath = this.GetPath();
            });

            this.BrowseDbPath = new RelayCommand(_ =>
            {
                this.DbPath = this.GetPath();
            });

            this.GenerateData = new RelayCommand(_ => {

                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                if (!this.TryGenerateData())
                {
                    MessageBox.Show("Failed to generate data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Mouse.OverrideCursor = null;
            });

            this.ClearList = new RelayCommand(_ =>
            {
                this.pcElemets.Clear();
                this.dbElemets.Clear();

            });
        }

        public ReadOnlyObservableCollection<PcData> PcElements { get; }

        public ReadOnlyObservableCollection<DbData> DbElements { get; }

        public ICommand BrowsePcPath { get; }

        public ICommand BrowseDbPath { get; }

        public ICommand GenerateData { get; }

        public ICommand ClearList { get; }

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
            if (Directory.Exists(this.PcPath))
            {
               this.TrySearchPcPrograms();
            }
            if (Directory.Exists(this.DbPath))
            {
                this.TrySearchDbFiles();
            }

            return true;
        }

        private bool TrySearchPcPrograms()
        {
            foreach (var file in Directory.GetFiles(this.PcPath))
            {
                if (PcData.TryParse(file, out var results))
                {
                    foreach (var item in results)
                    {
                        this.pcElemets.Add(item);
                    }
                }
            }

            return true;
        }

        private bool TrySearchDbFiles()
        {
            foreach (var file in Directory.GetFiles(this.DbPath))
            {
                if (DbData.TryParse(file, out var results))
                {
                    foreach (var item in results)
                    {
                        this.dbElemets.Add(item);
                    }
                }
            }

            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
