using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace Shared.DataModels 
{ 
     [TableName("jabatan")] 
     public class jabatan:BaseNotifyProperty  
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

          [DbColumn("Keterangan")] 
          public string Keterangan 
          { 
               get{return _keterangan;} 
               set{ 
                      _keterangan=value; 
                     OnPropertyChange("Keterangan");
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

          private int  _id;
           private string  _nama;
           private string  _keterangan;
           private int  _idbidang;
      }
}


