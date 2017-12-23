using Desktop.Collections;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Library;

namespace Desktop
{
   public class MainWindowViewModel
    {
        public Repository<pegawai> PegawaiCollection { get; }
        public Repository<bidang> BidangCollection { get; }
        public Repository<jabatan> JabatanCollection { get; }
        public Repository<absen> AbsenCollection { get; }
        public Repository<pegawai> AbsenTodayCollection { get; }
        public HubClient HubClient { get; }

        public MainWindowViewModel()
        {
            this.PegawaiCollection = new Repository<pegawai>("/api/pegawai");
            this.BidangCollection = new Repository<bidang>("/api/bidang");
            this.JabatanCollection = new Repository<jabatan>("/api/jabatan");
            this.AbsenCollection = new Repository<absen>("/api/absen");
            this.AbsenTodayCollection = new Repository<pegawai>("/api/absentoday");

            this.HubClient = new Library.HubClient();

        }
    }
}
