using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace Library.DataModels
{
    [TableName("absen")]
    public class absen :BaseNotify 
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        [DbColumn("JamMasuk")]
        public TimeSpan JamMasuk
        {
            get { return _jammasuk; }
            set
            {
                LamaKerja = JamPulang.Subtract(JamMasuk);
                SetProperty(ref _jammasuk, value);
            }
        }

        [DbColumn("JamPulang")]
        public TimeSpan JamPulang
        {
            get { return _jampulang; }
            set
            {
                
                LamaKerja = JamPulang.Subtract(JamMasuk);
                SetProperty(ref _jampulang, value);
            }
        }

        [DbColumn("Status")]
        public StatusKehadiran Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

      

        [DbColumn("PegawaiId")]
        public int PegawaiId
        {
            get { return _pegawaiid; }
            set
            {
                SetProperty(ref _pegawaiid, value);
            }
        }

        [DbColumn("PerizinanId")]
        public int PerizinanId
        {
            get { return _perizinanid; }
            set
            {
                SetProperty(ref _perizinanid , value);
            }
        }

        [DbColumn("PengaturanId")]
        public int PengaturanId
        {
            get { return _pengaturanid; }
            set
            {
                SetProperty(ref _pengaturanid, value);
            }
        }

        private DateTime dateTime;
        [DbColumn("Tanggal")]
        public DateTime Tanggal
        {
            get { return dateTime; }
            set { SetProperty(ref dateTime, value); }
        }


        private TimeSpan lamakerja;

        public TimeSpan LamaKerja
        {
            get { return lamakerja; }
            set { SetProperty(ref lamakerja, value); }
        }


        public pegawai Pegawai { get; set; }

        private int _id;
        private TimeSpan _jammasuk;
        private TimeSpan _jampulang;
        private StatusKehadiran _status;
        private int _pegawaiid;
        private int _perizinanid;
        private int _pengaturanid;
    }
}


