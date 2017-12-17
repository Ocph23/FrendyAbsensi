using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
   public  class AddNewJabatanViewModel:jabatan
    {
        private jabatan selectedItem;
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChange("Title");
            }
        }

        public AddNewJabatanViewModel()
        {
            Title = "Tambah Bidang Baru";
            SaveCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        public AddNewJabatanViewModel(jabatan selectedItem)
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
