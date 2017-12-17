using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace Library.DataModels 
{ 
     [TableName("pegawai")] 
     public class pegawai:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("NIP")] 
          public string NIP 
          { 
               get{return _nip;} 
               set{ 
                      _nip=value; 
                     OnPropertyChange("NIP");
                     }
          } 

          [DbColumn("JenisKelamin")] 
          public string JenisKelamin 
          { 
               get{return _jeniskelamin;} 
               set{ 
                      _jeniskelamin=value; 
                     OnPropertyChange("JenisKelamin");
                     }
          } 

          [DbColumn("TempatLahir")] 
          public string TempatLahir 
          { 
               get{return _tempatlahir;} 
               set{ 
                      _tempatlahir=value; 
                     OnPropertyChange("TempatLahir");
                     }
          } 

          [DbColumn("TanggalLahir")] 
          public DateTime TanggalLahir 
          { 
               get{return _tanggallahir;} 
               set{ 
                      _tanggallahir=value; 
                     OnPropertyChange("TanggalLahir");
                     }
          } 

          [DbColumn("Alamat")] 
          public string Alamat 
          { 
               get{return _alamat;} 
               set{ 
                      _alamat=value; 
                     OnPropertyChange("Alamat");
                     }
          } 

          [DbColumn("Telepon")] 
          public string Telepon 
          { 
               get{return _telepon;} 
               set{ 
                      _telepon=value; 
                     OnPropertyChange("Telepon");
                     }
          } 

          [DbColumn("IdJabatan")] 
          public int IdJabatan 
          { 
               get{return _idjabatan;} 
               set{ 
                      _idjabatan=value; 
                     OnPropertyChange("IdJabatan");
                     }
          } 

          [DbColumn("IdBidang")] 
          public int IdBidang 
          { 
               get{return _idbidang;} 
               set{ 
                      _idbidang=value; 
                     OnPropertyChange("IdBidang");
                     }
          } 

          [DbColumn("UserId")] 
          public string UserId 
          { 
               get{return _userid;} 
               set{ 
                      _userid=value; 
                     OnPropertyChange("UserId");
                     }
          } 

          private int  _id;
           private string  _nama;
           private string  _nip;
           private string  _jeniskelamin;
           private string  _tempatlahir;
           private DateTime  _tanggallahir;
           private string  _alamat;
           private string  _telepon;
           private int  _idjabatan;
           private int  _idbidang;
           private string  _userid;
      }
}


