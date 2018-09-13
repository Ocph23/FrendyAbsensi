using Desktop.Collections;
using Library.DataModels;
using System;
using Library;
using System.Drawing;
using System.Drawing.Imaging;
using DPUruNet;
using System.Linq;

namespace Desktop
{
    public class MainWindowViewModel
    {
        private Repository<pegawai> _absenTodayCollection;
        private Repository<perizinan> repositoryPerizinan;
        private Repository<pegawai> repositoryPegawai;
        private Repository<bidang> bidangCollection;
        private Repository<jabatan> jabatanCollection;
        private Repository<absen> absenCollection;
        private Repository<pegawai> pengawasCollection;
        private Repository<libur> _liburCollection;
        private Repository<pengaturan> _pengaturanCollection;
        public FingerPrint Finger { get; set; }

        public Repository<pengaturan> PengaturanCollection
        {
            get {
                if (_pengaturanCollection == null)
                    _pengaturanCollection = new Repository<pengaturan>("/api/pengaturan");
                return _pengaturanCollection; }
            set
            {
                _pengaturanCollection = value;
            }
        }

      //  private ReaderCollection _readers;

        public Repository<pegawai> AbsenTodayCollection
        {
            get
            {
                if (_absenTodayCollection == null)
                    _absenTodayCollection = new Repository<pegawai>("/api/absentoday");
                return _absenTodayCollection;
            }


        }
        public Repository<perizinan> PerizinanCollection
        {
            get
            {
                if (repositoryPerizinan == null)
                    repositoryPerizinan = new Repository<perizinan>("/api/perizinan");
                return repositoryPerizinan;
            }
        }
        public Repository<pegawai> PegawaiCollection
        {
            get
            {
                if (repositoryPegawai == null)
                    repositoryPegawai = new Repository<pegawai>("/api/pegawai");
                return repositoryPegawai;

            }
        }
        public Repository<bidang> BidangCollection
        {
            get
            {
                if (bidangCollection == null)
                    bidangCollection = new Repository<bidang>("/api/bidang");
                return bidangCollection;
            }
        }
        public Repository<jabatan> JabatanCollection
        {
            get
            {
                if (jabatanCollection == null)
                    jabatanCollection = new Repository<jabatan>("/api/jabatan");
                return jabatanCollection;
            }
        }
        public Repository<absen> AbsenCollection
        {
            get
            {
                if (absenCollection == null)
                    absenCollection = new Repository<absen>("/api/absen");
                return absenCollection;
            }
        }
        public Repository<pegawai> PengawasCollection
        {
            get
            {
                if (pengawasCollection == null)
                    pengawasCollection = new Repository<pegawai>("/api/setpengawas");
                return pengawasCollection;
            }
        }


        public Repository<libur> LiburCollection {
            get
            {
                if (_liburCollection == null)
                    _liburCollection = new Repository<libur>("/api/libur");

                return _liburCollection;
            }
        }

        public HubClient HubClient { get; set; }


      

        public MainWindowViewModel() {
           PengaturanCollection= new Repository<pengaturan>("/api/pengaturan");
         //  var res = ReaderCollection.GetReaders();
            Finger = new FingerPrint();



        }

        internal void StartSignalR()
        {
            HubClient = new HubClient(ResourcesBase.Token);
            HubClient.Start();
        }
    }




}
