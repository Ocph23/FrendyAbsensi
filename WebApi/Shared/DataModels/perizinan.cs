using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace Shared.DataModels 
{ 
     [TableName("perizinan")] 
     public class perizinan:BaseNotifyProperty  
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

          [DbColumn("Mulai")] 
          public DateTime Mulai 
          { 
               get{return _mulai;} 
               set{ 
                      _mulai=value; 
                     OnPropertyChange("Mulai");
                     }
          } 

          [DbColumn("Selesai")] 
          public DateTime Selesai 
          { 
               get{return _selesai;} 
               set{ 
                      _selesai=value; 
                     OnPropertyChange("Selesai");
                     }
          } 

          [DbColumn("Status")] 
          public string Status 
          { 
               get{return _status;} 
               set{ 
                      _status=value; 
                     OnPropertyChange("Status");
                     }
          } 

          [DbColumn("Catatan")] 
          public string Catatan 
          { 
               get{return _catatan;} 
               set{ 
                      _catatan=value; 
                     OnPropertyChange("Catatan");
                     }
          } 

          [DbColumn("PegawaiId")] 
          public int PegawaiId 
          { 
               get{return _pegawaiid;} 
               set{ 
                      _pegawaiid=value; 
                     OnPropertyChange("PegawaiId");
                     }
          } 

          [DbColumn("Jenis")] 
          public string Jenis 
          { 
               get{return _jenis;} 
               set{ 
                      _jenis=value; 
                     OnPropertyChange("Jenis");
                     }
          } 

          private int  _id;
           private DateTime  _mulai;
           private DateTime  _selesai;
           private string  _status;
           private string  _catatan;
           private int  _pegawaiid;
           private string  _jenis;
      }
}


