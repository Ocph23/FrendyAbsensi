using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph. DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Library.DataModels 
{ 
     [TableName("perizinan")] 
     public class perizinan:BaseNotify
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

          [DbColumn("Mulai")] 
          public DateTime Mulai 
          { 
               get{return _mulai;} 
               set{
                SetProperty(ref _mulai, value);
            }
          } 

          [DbColumn("Selesai")] 
          public DateTime Selesai 
          { 
               get{return _selesai;} 
               set{
                SetProperty(ref _selesai, value);
            }
          } 


          [DbColumn("Catatan")] 
          public string Catatan 
          { 
               get{return _catatan;} 
               set{
                SetProperty(ref _catatan, value);
            }
          } 

          [DbColumn("PegawaiId")] 
          public int PegawaiId 
          { 
               get{return _pegawaiid;} 
               set{
                SetProperty(ref _pegawaiid, value);
            }
          } 

          [DbColumn("Jenis")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusKehadiran Jenis 
          { 
               get{return _jenis;} 
               set{
                SetProperty(ref _jenis, value);
            }
          }

        public pegawai Pegawai { get; set; }

        private int  _id;
           private DateTime  _mulai;
           private DateTime  _selesai;
           private string  _catatan;
           private int  _pegawaiid;
           private StatusKehadiran  _jenis;
      }
}


