using Desktop.Collections;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
   public class MainWindowViewModel
    {
        public Repository<pegawai> PegawaiCollection { get; }
        public Repository<bidang> BidangCollection { get; }
        public Repository<jabatan> JabatanCollection { get; }
        public Repository<absen> AbsenCollection { get; }

        public MainWindowViewModel()
        {
            this.PegawaiCollection = new Repository<pegawai>("/api/pegawai");
            this.BidangCollection = new Repository<bidang>("/api/bidang");
            this.JabatanCollection = new Repository<jabatan>("/api/jabatan");
            this.AbsenCollection = new Repository<absen>("/api/absen");

        }
    }
}
