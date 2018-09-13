using Library.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
   public  class AddNewJabatanViewModel:jabatan,IDataErrorInfo
    {


        private string _title;
        private string error;
        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        private jabatan selectedItem;

        public AddNewJabatanViewModel()
        {
            Title = "Tambah Jabatan Baru";
            this.Load();
            
        }

        private void Load()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = x => string.IsNullOrEmpty(error), ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
            this.main = ResourcesBase.GetMainWindowViewModel();
            this.Bidangs = (CollectionView)CollectionViewSource.GetDefaultView(main.BidangCollection);
        }

        public AddNewJabatanViewModel(jabatan selectedItem)
        {
            this.Title = "Edit Jabatan";
            this.selectedItem = selectedItem;
            this.Load();
        }

        public CommandHandler SaveCommand { get; set; }
        public CommandHandler CancelCommand { get; set; }

        private MainWindowViewModel main;

        public CollectionView Bidangs { get; private set; }
        public Action WindowClose { get; set; }

        private void SaveCommandAction(object obj)
        {

            var item = new jabatan { Id = this.Id, IdBidang = IdBidang, Keterangan = Keterangan, Nama = Nama };

            if(Id<=0)
            {
                main.JabatanCollection.Add(item);
            }else
            {
                main.JabatanCollection.Updated(item);
            }
            WindowClose();

        }

        public string Error => error;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "IdBidang":
                        error = IdBidang <=0 ? "Pilih Bidang":null;
                        break;
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
    }
}
