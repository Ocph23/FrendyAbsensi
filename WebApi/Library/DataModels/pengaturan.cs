using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph. DAL;
 
 namespace Library.DataModels 
{ 
     [TableName("pengaturan")] 
     public class pengaturan:BaseNotify
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{
                SetProperty(ref _id, value);
            }
          } 

          [DbColumn("JamMasuk")] 
          public TimeSpan JamMasuk 
          { 
               get{return _jammasuk;} 
               set{
                SetProperty(ref _jammasuk , value);
            }
          } 

          [DbColumn("JamPulang")] 
          public TimeSpan JamPulang 
          { 
               get{return _jampulang;} 
               set{
                SetProperty(ref _jampulang , value);
            }
          }

        private double konpensasiTerlambat;
        [DbColumn("KonpensasiTerlambat")]
        public double KonpensasiTerlambat
        {
            get { return konpensasiTerlambat; }
            set { SetProperty(ref konpensasiTerlambat, value); }
        }


        private int  _id;
           private TimeSpan _jammasuk;
           private TimeSpan _jampulang;
      }
}


