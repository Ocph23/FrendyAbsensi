using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Laporan
    {
        public  Laporan()
        {

        }
        public Laporan(int totalDay)
        {
            TotalDay = totalDay;
        }
        public int Hadir { get; set; }
        public int Izin { get; set; }
        public int Sakit { get; set; }
        public int Cuti { get; set; }
        public int TugasBelajar { get; set; }
        public int DinasLuar { get; set; }
        public int TugasKedinasan { get; set; }
        public int Libur { get; set; }
        public int Alpha
        {
            get
            {
                return TotalDay -
                    (Hadir + Izin + Sakit + Cuti + TugasBelajar + DinasLuar + TugasKedinasan + Libur);
            }
        }

        public int TotalDay { get; set; }
        public int Terlambat { get; set; }
        public int Lembur { get; set; }
        public int CepatPulang { get; set; }
    }
}
