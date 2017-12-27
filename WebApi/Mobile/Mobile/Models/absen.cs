using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class absen : BaseViewModel
    {
       
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public TimeSpan JamMasuk
        {
            get { return _jammasuk; }
            set
            {
                LamaKerja = JamPulang.Subtract(JamMasuk);
                SetProperty(ref _jammasuk, value);
            }
        }

        public TimeSpan JamPulang
        {
            get { return _jampulang; }
            set
            {
                LamaKerja = JamPulang.Subtract(JamMasuk);
                SetProperty(ref _jampulang, value);
            }
        }

        public StatusKehadiran Status
        {
            get { return _status; }
            set
            {
                _status = value;
                SetProperty(ref _status, value);
            }
        }



        public int PegawaiId
        {
            get { return _pegawaiid; }
            set
            {
                SetProperty(ref _pegawaiid, value);
            }
        }

        public int PerizinanId
        {
            get { return _perizinanid; }
            set
            {
                SetProperty(ref _perizinanid, value);
            }
        }

        public int PengaturanId
        {
            get { return _pengaturanid; }
            set
            {
                SetProperty(ref _pengaturanid, value);
            }
        }

        private DateTime dateTime;
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
