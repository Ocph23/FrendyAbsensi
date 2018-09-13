using Desktop.Collections;
using Library.DataModels;
using Microsoft.Win32;
using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
    public class PegawaiViewModel:BaseNotify
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
                SetProperty(ref _selected, value);
            }
        }

        private string pengawasText;

        public string PengawasText
        {
            get { return pengawasText; }
            set { SetProperty(ref pengawasText, value); }
        }


        public PegawaiViewModel()
        {
            this.MainViewModel = ResourcesBase.GetMainWindowViewModel();
           MainPegawai=MainViewModel.PegawaiCollection;
            MainPegawai.SourceView.Refresh();
            AddCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = AddCommandAction };
            EditCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EditCommandAction };
            AddPengawasCommand = new CommandHandler { CanExecuteAction = AddPengawasCommandValidation, ExecuteAction = AddPengawasCommandAction };
            DeleteCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = DeleteCommandAction };
            ViewAbsenCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = ViewAbsenCommandAction };
            ChangeFotoCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = ChangeFotoAction };
            EnrollmentCommand = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = EnrollmentCommandAction };
        }

        private void EnrollmentCommandAction(object obj)
        {
            var form = new Forms.EnrollmentView(SelectedItem);
            form.ShowDialog();
        }



        private void ChangeFotoAction(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var file = openFileDialog.FileName;
                using(var st=new MemoryStream())
                {
                    Stream fs = File.OpenRead(file);
                    using (Image img = Image.FromStream(fs))
                    {
                        int h = 100;
                        int w = 100;
                        using (Bitmap b = new Bitmap(img, new Size(w, h)))
                        {
                            using (var mem2=new MemoryStream())
                            {
                                b.Save(mem2, ImageFormat.Jpeg);
                                SelectedItem.Photo = mem2.ToArray();
                            }
                        }
                    }

                  
                    
                    fs.Close();
                }


              

            }
           // SelectedItem = null;
            MainPegawai.SourceView.Refresh();

        }

        private void ViewAbsenCommandAction(object obj)
        {
           MainViewModel.AbsenCollection.Add(new absen
            {
                JamMasuk = new TimeSpan(8, 10, 30),
                JamPulang = new TimeSpan(16, 10, 30), PerizinanId=1,
                 PegawaiId=SelectedItem.Id, Pegawai=SelectedItem, PengaturanId=1, Status= Library.StatusKehadiran.Hadir, Tanggal=DateTime.Now
            });
            MainPegawai.SourceView.Refresh();
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
           var pegawai= MainViewModel.PengawasCollection.Updated(SelectedItem);
            SelectedItem.Pengawas = pegawai.Pengawas;
        }

        private void EditCommandAction(object obj)
        {
            var form = new Forms.AddNewPegawai();
            var viewmodel = new ViewModels.AddNewPegawaiViewModel(SelectedItem) { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            MainPegawai.SourceView.Refresh();
        }

        private void AddCommandAction(object obj)
        {
            var form = new Forms.AddNewPegawai();
            var viewmodel = new ViewModels.AddNewPegawaiViewModel() { WindowClose = form.Close };
            form.DataContext = viewmodel;
            form.ShowDialog();
            MainPegawai.SourceView.Refresh();
        }
        private void DeleteCommandAction(object obj)
        {
          MainPegawai.Remove(SelectedItem);
            MainPegawai.SourceView.Refresh();
        }

        public MainWindowViewModel MainViewModel { get; }
        public Repository<pegawai> MainPegawai { get; }
        public ObservableCollection<pegawai> Source { get; private set; }
        public CommandHandler AddCommand { get; }
        public CommandHandler EditCommand { get; }
        public CommandHandler AddPengawasCommand { get; }
        public CommandHandler DeleteCommand { get; }
        public CommandHandler ViewAbsenCommand { get; }
        public CommandHandler ChangeFotoCommand { get; }
        public CommandHandler EnrollmentCommand { get; }
    }
}
