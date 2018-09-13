using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModels
{
    [TableName("libur")]
   public class libur:BaseNotify
    {

        private int id;
        [DbColumn("Id")]
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        private DateTime tanggal;
        [DbColumn("Tanggal")]
        public DateTime Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; SetProperty(ref tanggal, value); }
        }

        private string keterangan;

        [DbColumn("Keterangan")]
        public string Keterangan
        {
            get { return keterangan; }
            set { SetProperty(ref keterangan, value); }
        }


    }
}
