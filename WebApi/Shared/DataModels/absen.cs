using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace Shared.DataModels 
{ 
     [TableName("absen")] 
     public class absen:BaseNotifyProperty  
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

          [DbColumn("JamMasuk")] 
          public DateTime JamMasuk 
          { 
               get{return _jammasuk;} 
               set{ 
                      _jammasuk=value; 
                     OnPropertyChange("JamMasuk");
                     }
          } 

          [DbColumn("JamPulang")] 
          public DateTime JamPulang 
          { 
               get{return _jampulang;} 
               set{ 
                      _jampulang=value; 
                     OnPropertyChange("JamPulang");
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

          [DbColumn("JamIstirahat")] 
          public DateTime JamIstirahat 
          { 
               get{return _jamistirahat;} 
               set{ 
                      _jamistirahat=value; 
                     OnPropertyChange("JamIstirahat");
                     }
          } 

          [DbColumn("JamMasukIstirahat")] 
          public DateTime JamMasukIstirahat 
          { 
               get{return _jammasukistirahat;} 
               set{ 
                      _jammasukistirahat=value; 
                     OnPropertyChange("JamMasukIstirahat");
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

          [DbColumn("PerizinanId")] 
          public int PerizinanId 
          { 
               get{return _perizinanid;} 
               set{ 
                      _perizinanid=value; 
                     OnPropertyChange("PerizinanId");
                     }
          } 

          [PrimaryKey("PengaturanId")] 
          [DbColumn("PengaturanId")] 
          public int PengaturanId 
          { 
               get{return _pengaturanid;} 
               set{ 
                      _pengaturanid=value; 
                     OnPropertyChange("PengaturanId");
                     }
          } 

          private int  _id;
           private DateTime  _jammasuk;
           private DateTime  _jampulang;
           private string  _status;
           private DateTime  _jamistirahat;
           private DateTime  _jammasukistirahat;
           private int  _pegawaiid;
           private int  _perizinanid;
           private int  _pengaturanid;
      }
}


