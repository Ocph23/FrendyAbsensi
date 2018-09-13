using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataModels;
using System.Windows.Data;
using Desktop.Collections;

namespace Desktop.ViewModels
{
  public class PerizinanViewModel:BaseViewModel
    {
        private perizinan selectedItem;

        public perizinan SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem , value); }
        }


        public PerizinanViewModel()
        {
            AddCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddCommandAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = DeleteCommandAction };
            MainCollection= ResourcesBase.GetMainWindowViewModel().PerizinanCollection;
            MainCollection.OnChangeSource += Data_OnChangeSource;
            MainCollection.SourceView.Refresh();

        }

        private void Data_OnChangeSource(perizinan obj)
        {
            MainCollection.SourceView.Refresh();
        }

        public override void RefreshCommandAction(object obj)
        {
            base.RefreshCommandAction(obj);
            MainCollection.SourceView.Refresh();
        }
       

        private void EditCommandAction(object obj)
        {
            var form = new Forms.AddperizinanView();
            var vm = new ViewModels.AddperizinanViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
        }

        private void DeleteCommandAction(object obj)
        {
            var sure = ResourcesBase.MessageAsk("Yakin Hapus Data ?");
            if(sure)
            {

            }
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddperizinanView();
            var vm = new ViewModels.AddperizinanViewModel() { WindowClose=form.Close};
            form.DataContext = vm;
            form.ShowDialog();
        }

        public CommandHandler AddCommand { get; }
        public CommandHandler EditCommand { get; }
        public CommandHandler DeleteCommand { get; }
        public Repository<perizinan> MainCollection { get; }
    }
}
