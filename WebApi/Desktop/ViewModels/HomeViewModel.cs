using Desktop.Collections;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
   public  class HomeViewModel:BaseViewModel
    {
        private DateTime dateTime;
        private pegawai _selected;
        public Repository<pegawai> MainCollection { get; }

        public DateTime Today
        {
            get { return dateTime; }
            set { SetProperty(ref dateTime, value); }
        }

        public CommandHandler SetAbsenCommand { get; }
        public pegawai SelectedItem
        {
            get
            {
                return _selected;
            }
            set
            {
                SetProperty(ref _selected, value);
            }
        }

        public HomeViewModel()
        {
            MainCollection = ResourcesBase.GetMainWindowViewModel().AbsenTodayCollection;
           
            RingProgressActive = true;
            Today = DateTime.Now;
            MainCollection.SourceView.Refresh();
            SetAbsenCommand = new CommandHandler { CanExecuteAction =x=>true, ExecuteAction = SetAbsenAction };
            RingProgressActive = false;
        }


       

        public  override void RefreshCommandAction(object obj)
        {
            RingProgressActive = true;
            base.RefreshCommandAction(obj);
            MainCollection.SourceView.Refresh();
            RingProgressActive = false;

        }


        private void SetAbsenAction(object obj)
        {
            if (SelectedItem != null)
            {
                if (string.IsNullOrEmpty(SelectedItem.Enrollment))
                {
                    ResourcesBase.ShowMessageError("Sidik Jari Belum Didaftarkan");
                }
                else
                {
                    var form = new Forms.AbsenVerification(SelectedItem);
                    form.Show();
                }
            }
        }
    }
}
