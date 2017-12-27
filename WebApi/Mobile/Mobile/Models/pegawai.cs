using Mobile.Models;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace Mobile.Models
{ 
     public class pegawai:BaseViewModel
   {
         
          public int Id 
          { 
               get{return _id;}
            set
            {
                SetProperty(ref _id, value);
            }
          } 

          public string Nama 
          { 
               get{return _nama;} 
               set{
                SetProperty(ref _nama, value);
            }
          } 

          public string NIP 
          { 
               get{return _nip;} 
               set{
                SetProperty(ref _nip, value);
            }
          } 

          public Gender JenisKelamin 
          { 
               get{return _jeniskelamin;} 
               set{
                SetProperty(ref _jeniskelamin, value);
            }
          } 

          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{
                SetProperty(ref _tempatlahir, value);
            }
          } 

          public DateTime TanggalLahir 
          { 
               get{return _tanggallahir;} 
               set{
                SetProperty(ref _tanggallahir, value);
            }
          } 

          public string Alamat 
          { 
               get{return _alamat;} 
               set{
                SetProperty(ref _alamat, value);
            }
          } 

          public string Telepon 
          { 
               get{return _telepon;} 
               set{
                SetProperty(ref _telepon, value);
            }
          } 

          public int IdJabatan 
          { 
               get{return _idjabatan;} 
               set{
                SetProperty(ref _idjabatan, value);
            }
          } 

          public int IdBidang 
          { 
               get{return _idbidang;} 
               set{
                SetProperty(ref _idbidang, value);
            }
          } 

           
        private string email;
        public string Email
        {
            get { return email; }
            set {
                SetProperty(ref email, value);
            }
        }

        private string golongan;
        public string Golongan
        {
            get { return golongan; }
            set {
                SetProperty(ref golongan, value);
            }
        }



        
          public string UserId 
          { 
               get{return _userid;} 
               set{
                SetProperty(ref _userid, value);
            }
          }

        public bool Pengawas
        {
            get { return _pengawas; }
            set
            {
                SetProperty(ref _pengawas, value);
            }
        }




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
    }
}


