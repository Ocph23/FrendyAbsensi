using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace Library.DataModels 
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

          private int  _id;
           private DateTime _jammasuk;
           private DateTime _jampulang;
      }
}


