using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Collections;
using Library.DataModels;
using Ocph.DAL;

namespace Desktop.ViewModels
{
     public   class PengaturanViewModel:pengaturan
    {
        public PengaturanViewModel()
        {
            SaveCommand = new CommandHandler { CanExecuteAction = SaveValidate, ExecuteAction = SaveAction };
            var main = ResourcesBase.GetMainWindowViewModel();
            MainCollection = main.PengaturanCollection;
            MainCollection.OnChangeSource += MainCollection_OnChangeSource;
            MainCollection.SourceView.Refresh();
            RingProgressActive = true;
            LoadAsync();
        }

        private void MainCollection_OnChangeSource(pengaturan obj)
        {
           if(obj!=null)
            {
                this.Id = obj.Id;
                this.JamMasuk = obj.JamMasuk;
                this.JamPulang = obj.JamPulang;
            }
        }

        private bool SaveValidate(object obj)
        {

            if (this.JamMasuk < new TimeSpan(7, 0, 0) || this.JamPulang > new TimeSpan(18, 0, 0))
                return false;
            if (this.JamMasuk > this .JamPulang)
                return false;
            if (this.KonpensasiTerlambat > 59)
                return false;
            return true;
        }

        private void SaveAction(object obj)
        {

            MainCollection.Add(new pengaturan {JamMasuk=JamMasuk, JamPulang=JamPulang, KonpensasiTerlambat=KonpensasiTerlambat });
        }

        private void LoadAsync()
        {
            if (MainCollection.Count > 0)
            {
               var data = MainCollection.Last();
                KonpensasiTerlambat = data.KonpensasiTerlambat;
                Id = data.Id;
                JamMasuk = data.JamMasuk;
                JamPulang = data.JamPulang;
            }
            RingProgressActive = false;
        }

        public CommandHandler SaveCommand { get; }
        public Repository<pengaturan> MainCollection { get; set; }


        private bool _ring;

        public bool RingProgressActive
        {
            get { return _ring; }
            set { SetProperty(ref _ring , value); }
        }


    }
}
