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
    class JabatanViewModel:DAL.BaseNotifyProperty
    {
        private jabatan _jabatan;

        public ObservableCollection<jabatan> Source { get; set; }
        public CollectionView SourceView { get; set; }
        public CommandHandler AddCommand { get; }
        public CommandHandler EditCommand { get; }
        public CommandHandler DeleteCommand { get; }

        public jabatan SelectedItem
        {
            get
            {
                return _jabatan;
            }
            set
            {
                _jabatan = value;
                OnPropertyChange("SelectedItem");
            }
        }

        public JabatanViewModel()
        {
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(ResourcesBase.GetMainWindowViewModel().JabatanCollection);

            AddCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddCommandAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = DeleteCommandAction };
        }

        private void DeleteCommandAction(object obj)
        {
            ResourcesBase.GetMainWindowViewModel().JabatanCollection.Remove(SelectedItem);
            SourceView.Refresh();
        }

        private void EditCommandAction(object obj)
        {
            var form = new Forms.AddNewJabatan();
            var vm = new ViewModels.AddNewJabatanViewModel(SelectedItem);
            form.DataContext = vm;
            form.ShowDialog();
            SourceView.Refresh();
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddNewJabatan();
            var vm = new ViewModels.AddNewJabatanViewModel() { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
            SourceView.Refresh();
        }
    }
}
