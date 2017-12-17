using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
    public class PegawaiViewModel:DAL.BaseNotifyProperty
    {
        private pegawai _selected;

        public pegawai SelectedItem
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChange("SelectedItem");
            }
        }

        public PegawaiViewModel()
        {
            Source = new ObservableCollection<pegawai>(ResourcesBase.GetMainWindowViewModel().PegawaiCollection);
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
        }

        public ObservableCollection<pegawai> Source { get; private set; }
        public CollectionView SourceView { get; private set; }
    }
}
