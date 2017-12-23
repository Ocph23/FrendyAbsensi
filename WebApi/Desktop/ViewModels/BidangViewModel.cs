using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
   public class BidangViewModel:DAL.BaseNotifyProperty
    {
        private bidang _bidang;
        public CollectionView SourceView { get; set; }
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
                _bidang = value;
                OnPropertyChange("SelectedItem");
            }
        }


        public BidangViewModel()
        {
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(ResourcesBase.GetMainWindowViewModel().BidangCollection);

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
            SourceView.Refresh();
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddNewBidang();
            var vm = new ViewModels.AddNewBidangViewModel() { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
            SourceView.Refresh();
        }
        private void DeleteCommandAction(object obj)
        {
            ResourcesBase.GetMainWindowViewModel().BidangCollection.Remove(SelectedItem);
            SourceView.Refresh();
        }
    }
}
