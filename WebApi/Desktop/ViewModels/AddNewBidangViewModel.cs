using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataModels;

namespace Desktop.ViewModels
{
   public  class AddNewBidangViewModel:Library.DataModels.bidang
    {
        private bidang selectedItem;
        private string _title;

        public string Title { get { return _title; }
            set
            {
                _title = value;
                OnPropertyChange("Title");
            }
        }

        public AddNewBidangViewModel()
        {
            Title = "Tambah Bidang Baru";
            SaveCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction =x=> WindowClose() };
        }

        public AddNewBidangViewModel(bidang selectedItem)
        {
            this.Title = "Edit Bidang";
            this.selectedItem = selectedItem;
        }

        public CommandHandler SaveCommand { get; }
        public CommandHandler CancelCommand { get; }
        public Action WindowClose { get; set; }

        private void SaveCommandAction(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
