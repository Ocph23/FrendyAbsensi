using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace Shared.DataModels 
{ 
     [TableName("pengaturan")] 
     public class pengaturan:BaseNotifyProperty  
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

          [DbColumn("HitungLembur")] 
          public string HitungLembur 
          { 
               get{return _hitunglembur;} 
               set{ 
                      _hitunglembur=value; 
                     OnPropertyChange("HitungLembur");
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
          public 0 JamPulang 
          { 
               get{return _jampulang;} 
               set{ 
                      _jampulang=value; 
                     OnPropertyChange("JamPulang");
                     }
          } 

          private int  _id;
           private string  _hitunglembur;
           private 0  _jammasuk;
           private 0  _jampulang;
      }
}


