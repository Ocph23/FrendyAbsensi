using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace Library.DataModels 
{ 
     [TableName("pegawai")] 
     public class pegawai:BaseNotify
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{
                SetProperty(ref  _id, value);
            }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{
                SetProperty(ref _nama , value);
            }
          } 

          [DbColumn("NIP")] 
          public string NIP 
          { 
               get{return _nip;} 
               set{
                SetProperty(ref _nip, value);
            }
          } 

          [DbColumn("JenisKelamin")] 
          public Gender JenisKelamin 
          { 
               get{return _jeniskelamin;} 
               set{
                SetProperty(ref _jeniskelamin, value);
            }
          } 

          [DbColumn("TempatLahir")] 
          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{
                SetProperty(ref _tempatlahir , value);
            }
          } 

          [DbColumn("TanggalLahir")] 
          public DateTime TanggalLahir 
          { 
               get{return _tanggallahir;} 
               set{
                SetProperty(ref _tanggallahir , value);
            }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{
                SetProperty(ref _alamat , value);
            }
          } 

          [DbColumn("Telepon")] 
          public string Telepon 
          { 
               get{return _telepon;} 
               set{
                SetProperty(ref _telepon , value);
            }
          } 

          [DbColumn("IdJabatan")] 
          public int IdJabatan 
          { 
               get{return _idjabatan;} 
               set{
                SetProperty(ref _idjabatan , value);
            }
          } 

          [DbColumn("IdBidang")] 
          public int IdBidang 
          { 
               get{return _idbidang;} 
               set{
                SetProperty(ref _idbidang , value);
            }
          } 

           
        private string email;
        [DbColumn("Email")]
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string golongan;
        [DbColumn("Golongan")]
        public string Golongan
        {
            get { return golongan; }
            set { SetProperty(ref golongan , value); }
        }



        [DbColumn("UserId")] 
          public string UserId 
          { 
               get{return _userid;} 
               set{
                SetProperty(ref _userid, value);
            }
          }

        [DbColumn("Pengawas")]
        public bool Pengawas
        {
            get { return _pengawas; }
            set
            {
                SetProperty(ref _pengawas, value);
            }
        }

        private byte[] photo;
        [DbColumn("Photo")]
        public byte[] Photo
        {
            get { return photo; }
            set { photo = value;
                SetProperty(ref photo, value);
            }
        }

        [DbColumn("Enrollment")]
        public string Enrollment
        {
            get { return enrollment; }
            set
            {
                SetProperty(ref enrollment, value);
            }
        }
        

        public virtual List<absen> Absens { get; set; }
        public virtual Laporan Sumarry { get; set; }

        public jabatan Jabatan { get; set; }
        public bidang Bidang { get; set; }
        public absen AbsenToday { get; set; }

        private int  _id;
           private string  _nama;
           private string  _nip;
           private Gender  _jeniskelamin;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private string  _alamat;
           private string  _telepon;
           private int  _idjabatan;
           private int  _idbidang;
           private string  _userid;
        private bool _pengawas;
        private string enrollment;
    }
}


