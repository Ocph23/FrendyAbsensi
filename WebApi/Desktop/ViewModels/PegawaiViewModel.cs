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
            this.MainViewModel = ResourcesBase.GetMainWindowViewModel();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(ResourcesBase.GetMainWindowViewModel().PegawaiCollection);
            AddCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddCommandAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = DeleteCommandAction };
        }

        private void EditCommandAction(object obj)
        {
            var form = new Forms.AddNewPegawai();
            var viewmodel = new ViewModels.AddNewPegawaiViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            SourceView.Refresh();
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddNewPegawai();
            var viewmodel = new ViewModels.AddNewPegawaiViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            SourceView.Refresh();
        }
        private void DeleteCommandAction(object obj)
        {
            ResourcesBase.GetMainWindowViewModel().PegawaiCollection.Remove(SelectedItem);
            SourceView.Refresh();
        }

        public MainWindowViewModel MainViewModel { get; }
        public ObservableCollection<pegawai> Source { get; private set; }
        public CollectionView SourceView { get; private set; }
        public CommandHandler AddCommand { get; }
        public CommandHandler EditCommand { get; }
        public CommandHandler DeleteCommand { get; }
    }
}
