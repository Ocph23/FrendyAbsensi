using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataModels;
using System.ComponentModel;

namespace Desktop.ViewModels
{
   public  class AddNewBidangViewModel:Library.DataModels.bidang,IDataErrorInfo
    {
        private bidang selectedItem;
        private string _title;
        private string error;
        public string Title { get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        public AddNewBidangViewModel()
        {
            Title = "Tambah Bidang Baru";
            SaveCommand = new CommandHandler { CanExecuteAction = x => string.IsNullOrEmpty(error), ExecuteAction = SaveCommandAction };
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

        public string Error =>error;

        public string this[string columnName] {
            get {
                error = string.Empty;
                switch (columnName)
                {
                    case "Nama":
                        error = string.IsNullOrEmpty(this.Nama) ? "Nama Tidak Boleh Kosong" : null;
                        break;
                    case "Keterangan":
                        error = string.IsNullOrEmpty(this.Keterangan) ? "Keterangan tidak boleh Kosong" : null;
                        break;
                    default:
                        break;
                }

                return error;
            }
        }

        private void SaveCommandAction(object obj)
        {
            var main = ResourcesBase.GetMainWindowViewModel();
            bidang item = new bidang { Id = this.Id, Keterangan = this.Keterangan, Nama = Nama };

            if(this.Id<=0)
            {
               main.BidangCollection.Add(item);
               
            }else
            {
                main.BidangCollection.Updated(item);
            }
            this.WindowClose();
        }
    }
}
