using System;
using Library.DataModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desktop.ViewModels
{
    internal class AddNewPegawaiViewModel:pegawai,IDataErrorInfo
    {
        private string _title;
        private pegawai selectedItem;
        private string error;

        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        public AddNewPegawaiViewModel()
        {
            this.MainCollection = ResourcesBase.GetMainWindowViewModel();
            Title = "Tambah Pegawai Baru";
            this.Load();
        }

        private void Load()
        {
           // this.MainCollection = ResourcesBase.GetMainWindowViewModel();
            SaveCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        public AddNewPegawaiViewModel(pegawai selectedItem)
        {
            Title = "Edit Pegawai Baru";
            this.selectedItem = selectedItem;
            this.Load();
        }

        public Action WindowClose { get; internal set; }
        public MainWindowViewModel MainCollection { get; private set; }
        public CommandHandler SaveCommand { get; set; }
        public CommandHandler CancelCommand { get; set; }

        public string Error => error;

        public string this[string columnName]
        {
            get
            {
                error = string.Empty;
                switch (columnName)
                {
                    case "Nama":
                       error= string.IsNullOrEmpty(Nama) ?  "Nama Tidak Boleh Kosong" : null;
                        break;

                    case "NIP":
                        error = string.IsNullOrEmpty(NIP) ? "NIP Tidak Boleh Kosong" : null;
                        break;

                    case "IdBidang":
                        error = IdBidang<=0 ? "Pilih Bidang" : null;
                        break;

                    case "IdJabatan":
                        error = IdJabatan <=0 ? "Nama Tidak Boleh Kosong" : null;
                        break;
                    case "Golongan":
                        error = string.IsNullOrEmpty(Golongan)? "Golongan Tidak Boleh Kosong" : null;
                        break;

                    case "TempatLahir":
                        error = string.IsNullOrEmpty(TempatLahir) ? "Tempat Lahir Tidak Boleh Kosong" : null;
                        break;
                    case "Email":
                        error = !new EmailAddressAttribute().IsValid(Email) || string.IsNullOrEmpty(Email) ? "Email Tidak Valid":null ;
                        break;

                    case "Telepon":
                        error = string.IsNullOrEmpty(Telepon) ? "Nomor Telepon Tidak Boleh Kosong" : null;
                        break;

                    case "TanggalLahir":
                        error = TanggalLahir < new DateTime(1950,12,01) ? "Tanggal Lahir Tidak Bo;eh Kosong" : null;
                        break;
                    case "Alamat":
                        error = string.IsNullOrEmpty(Alamat) ? "Alamat Tidak Boleh Kosong" : null;
                        break;

                    default:
                        break;
                }


                return error;
            }
        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        private void SaveCommandAction(object obj)
        {
            
           if(Id==0)
            {
                pegawai pegawa = new pegawai
                {
                    Alamat = Alamat,
                    Id = Id,
                    IdBidang = IdBidang,
                    IdJabatan = IdJabatan,
                    JenisKelamin = JenisKelamin,
                    Nama = Nama,
                    NIP = NIP,
                    TanggalLahir = TanggalLahir,
                    Telepon = Telepon,
                    TempatLahir = TempatLahir,
                    Email=Email,Golongan=Golongan,
                    UserId = UserId
                };
                ResourcesBase.GetMainWindowViewModel().PegawaiCollection.Add(pegawa);
            }else
            {
                pegawai pegawa = new pegawai
                {
                    Alamat = Alamat,
                    Id = Id,
                    IdBidang = IdBidang,
                    IdJabatan = IdJabatan,
                    JenisKelamin = JenisKelamin,
                    Nama = Nama,
                    NIP = NIP,
                    TanggalLahir = TanggalLahir,
                    Telepon = Telepon,
                    TempatLahir = TempatLahir,
                    Email = Email,
                    Golongan = Golongan,
                    UserId = UserId
                };
                ResourcesBase.GetMainWindowViewModel().PegawaiCollection.Updated(pegawa);
            }

            this.WindowClose();
        }


    }
}