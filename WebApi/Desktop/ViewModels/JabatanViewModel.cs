using Desktop.Collections;
using Library.DataModels;
using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
    class JabatanViewModel:BaseNotify
    {
        private jabatan _jabatan;

       
        public Repository<jabatan> MainCollection { get; }
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
                SetProperty(ref _jabatan, value);
            }
        }

        public JabatanViewModel()
        {
            MainCollection=ResourcesBase.GetMainWindowViewModel().JabatanCollection;

            AddCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddCommandAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = DeleteCommandAction };
        }

        private void DeleteCommandAction(object obj)
        {
            MainCollection.Remove(SelectedItem);
            MainCollection.SourceView.Refresh();
        }

        private void EditCommandAction(object obj)
        {
            var form = new Forms.AddNewJabatan();
            var vm = new ViewModels.AddNewJabatanViewModel(SelectedItem);
            form.DataContext = vm;
            form.ShowDialog();
            MainCollection.SourceView.Refresh();
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddNewJabatan();
            var vm = new ViewModels.AddNewJabatanViewModel() { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
            MainCollection.SourceView.Refresh();
        }
    }
}
