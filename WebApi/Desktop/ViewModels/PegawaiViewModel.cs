using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
    public class PegawaiViewModel:DAL.BaseNotifyProperty
    {
        private pegawai _selected;

        public pegawai SelectedItem
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChange("SelectedItem");
            }
        }

        private string pengawasText;

        public string PengawasText
        {
            get { return pengawasText; }
            set { pengawasText = value; OnPropertyChange("PengawasText"); }
        }


        public PegawaiViewModel()
        {
            this.MainViewModel = ResourcesBase.GetMainWindowViewModel();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(ResourcesBase.GetMainWindowViewModel().PegawaiCollection);
            AddCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddCommandAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditCommandAction };
            AddPengawasCommand = new CommandHandler { CanExecuteAction = AddPengawasCommandValidation, ExecuteAction = AddPengawasCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = DeleteCommandAction };
            ViewAbsenCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = ViewAbsenCommandAction };
            
        }

        private void ViewAbsenCommandAction(object obj)
        {
            ResourcesBase.GetMainWindowViewModel().AbsenCollection.Add(new absen
            {
                JamMasuk = new TimeSpan(8, 10, 30),
                JamPulang = new TimeSpan(16, 10, 30), PerizinanId=1,
                 PegawaiId=SelectedItem.Id, Pegawai=SelectedItem, PengaturanId=1, Status= Library.StatusKehadiran.Hadir, Tanggal=DateTime.Now
            });
        }

        private bool AddPengawasCommandValidation(object obj)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.Pengawas)
                    PengawasText = "Hapus Sebagai Pengawas";
                else
                    PengawasText = "Tambah Sebagai Pengawas";
                return true;
            }
            else
                return false;
        }

        private void AddPengawasCommandAction(object obj)
        {
           var pegawai= ResourcesBase.GetMainWindowViewModel().PengawasCollection.Updated(SelectedItem);
            SelectedItem.Pengawas = pegawai.Pengawas;
        }

        private void EditCommandAction(object obj)
        {
            var form = new Forms.AddNewPegawai();
            var viewmodel = new ViewModels.AddNewPegawaiViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            SourceView.Refresh();
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddNewPegawai();
            var viewmodel = new ViewModels.AddNewPegawaiViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            SourceView.Refresh();
        }
        private void DeleteCommandAction(object obj)
        {
            ResourcesBase.GetMainWindowViewModel().PegawaiCollection.Remove(SelectedItem);
            SourceView.Refresh();
        }

        public MainWindowViewModel MainViewModel { get; }
        public ObservableCollection<pegawai> Source { get; private set; }
        public CollectionView SourceView { get; private set; }
        public CommandHandler AddCommand { get; }
        public CommandHandler EditCommand { get; }
        public CommandHandler AddPengawasCommand { get; }
        public CommandHandler DeleteCommand { get; }
        public CommandHandler ViewAbsenCommand { get; }
    }
}
