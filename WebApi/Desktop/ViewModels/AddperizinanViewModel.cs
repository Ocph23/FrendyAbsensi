using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataModels;
using Library;

namespace Desktop.ViewModels
{
    public class AddperizinanViewModel: Library.DataModels.perizinan,IDataErrorInfo
    {
        private string error;

        public AddperizinanViewModel()
        {
            Mulai = DateTime.Now;
            Selesai = DateTime.Now;
            LoadAsync();
        }

        private void LoadAsync()
        {
            MainVM = ResourcesBase.GetMainWindowViewModel();
            Jenises = ResourcesBase.GetEnumCollection<StatusKehadiran>();
            SaveCommand = new CommandHandler { CanExecuteAction = SaveValidate, ExecuteAction = SaveCommandAction };
            CloseCommand = new CommandHandler { CanExecuteAction =x=>true, ExecuteAction =x=> WindowClose() };
            MainVM.PerizinanCollection.OnChangeSource += PerizinanCollection_OnChangeSource;
        }
        

        private void PerizinanCollection_OnChangeSource(perizinan obj)
        {
            WindowClose();
        }

        private bool SaveValidate(object obj)
        {
            if (!string.IsNullOrEmpty(error))
                return false;
            if (Selesai < Mulai)
                return false;

            return true;
        }

        public AddperizinanViewModel(perizinan selectedItem)
        {
            LoadAsync();
            this.Pegawai = selectedItem.Pegawai;
            this.Id= selectedItem.Id;
            this.Catatan = selectedItem.Catatan;
            this.Jenis = selectedItem.Jenis;
            this.Mulai = selectedItem.Mulai;
           
            this.PegawaiId = selectedItem.PegawaiId;
            this.Selesai = selectedItem.Selesai;
            this.SelectedItem = selectedItem;
        }

        private void SaveCommandAction(object obj)
        {
            var item = new perizinan { Catatan = Catatan, Id = Id, Jenis = Jenis, Mulai = Mulai, PegawaiId = PegawaiId, Selesai = Selesai};
            if (item.Id <= 0)
                MainVM.PerizinanCollection.Add(item);
            else
            {
                var res=    MainVM.PerizinanCollection.Updated(item);
                if(res!=null)
                {
                    SelectedItem.Catatan = item.Catatan;
                    SelectedItem.Jenis = item.Jenis;
                    SelectedItem.Mulai = item.Mulai;
                    SelectedItem.Selesai = item.Selesai;
                }
            }
    
        }

        public MainWindowViewModel MainVM { get; set; }
        public List<StatusKehadiran> Jenises { get; set; }
        public CommandHandler SaveCommand { get; set; }
        public CommandHandler CloseCommand { get; private set; }

        public string Error => error;

        public Action WindowClose { get; internal set; }
        public perizinan SelectedItem { get; }

        public string this[string columnName] {
            get
            {
                error = string.Empty;
                if (columnName == "Mulai")
                    error = this.Mulai == new DateTime() ? "Tentukan Tanggal Mulai" : null;

                return error;

            }

        }
    }
}
