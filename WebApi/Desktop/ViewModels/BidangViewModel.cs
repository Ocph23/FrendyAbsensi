using Desktop.Collections;
using Library.DataModels;
using Ocph.DAL;

namespace Desktop.ViewModels
{
    public class BidangViewModel:BaseNotify
    {
        private bidang _bidang;
        public CommandHandler AddCommand { get; }
        public CommandHandler EditCommand { get; }
        public CommandHandler DeleteCommand { get; }

        public bidang SelectedItem {
            get
            {
                return _bidang;
            }
            set
            {
                SetProperty(ref _bidang, value);
            }
        }

        public Repository<bidang> MainCollection { get; }

        public BidangViewModel()
        {
            MainCollection = ResourcesBase.GetMainWindowViewModel().BidangCollection;
            MainCollection.SourceView.Refresh();
            AddCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddCommandAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = DeleteCommandAction };
        }

        private void EditCommandAction(object obj)
        {
            var form = new Forms.AddNewBidang();
            var vm = new ViewModels.AddNewBidangViewModel(SelectedItem);
            form.DataContext = vm;
            form.ShowDialog();
           MainCollection.SourceView.Refresh();
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddNewBidang();
            var vm = new ViewModels.AddNewBidangViewModel() { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
            MainCollection.SourceView.Refresh();
        }
        private void DeleteCommandAction(object obj)
        {
            MainCollection.Remove(SelectedItem);
            MainCollection.SourceView.Refresh();
        }
    }
}
