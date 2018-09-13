using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Library.DataModels;
using Library;

namespace WebApi.Models
{
    public class KehadiranContext
    {
        private DateTime start;
        private DateTime end;
        private int Saturdays;
        private int Sundays;
        public List<libur> LiburCollection = new List<libur>();

        public KehadiranContext(DateTime start ,DateTime end)
        {
            this.start = start;
            this.end = end;
            InitAsync();
        }

        public KehadiranContext()
        {

        }

        public DateTime Start { get { return start; } }
        public DateTime End { get { return end; } }

        public  void InitAsync()
        {
            if(start!=null && end!=null)
            {
                using (var db = new OcphDbContext())
                {
                    var liburs = db.Libur.Where(O => O.Tanggal >= start && O.Tanggal <= end).ToList();
                    foreach (var item in liburs)
                    {
                        LiburCollection.Add(item);
                    }
                }

                var newDate = new DateTime(start.Year, start.Month, start.Day);
                while (newDate <= End)
                {
                    if (newDate.DayOfWeek == DayOfWeek.Saturday)
                        Saturdays++;
                    else if (newDate.DayOfWeek == DayOfWeek.Sunday)
                        Sundays++;
                    newDate = newDate.AddDays(1);
                }

            }
           
        }

        internal Tuple<int,int,int> HitungTerlambat(List<absen> res)
        {
            int terlambat = 0;
            int lembur = 0;
            int cepat = 0;
            using (var db = new OcphDbContext())
            {
                var set = db.Setting.GetLastItem();
                if (set != null)
                {
                    foreach(var item in res)
                    {
                        var harusMasuk =(int) item.JamMasuk.Subtract(set.JamMasuk).TotalMinutes;
                        if (harusMasuk > 0)
                            terlambat += harusMasuk;

                        var haruspulang = (int)item.JamPulang.Subtract(set.JamPulang).TotalMinutes;
                        if (haruspulang > 0)
                            lembur += haruspulang;
                        else
                            cepat += ((haruspulang) * -1);

                    }
                }
              
                    return Tuple.Create(terlambat,cepat,lembur);
            }
        }

        public int TotalLibur
        {
            get
            {
                return Saturdays + Sundays + LiburCollection.Count;
            }


        }

        //Single

        public bool IsHoliday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                return true;
            else
            {
                using (var db = new OcphDbContext())
                {
                    var res = db.Libur.Where(O => O.Tanggal == date).FirstOrDefault();
                    if (res != null)
                        return true;
                }
            }
            return false;
        }

        public Laporan Perizinan(DateTime date,int PegawaiId)
        {

            using (var db = new OcphDbContext())
            {
                Laporan lap = new Laporan(1);
                var result = db.Perizinan.Where(O => O.PegawaiId == PegawaiId && O.Mulai <= date && O.Selesai >= date);
                if(result!=null)
                {
                    foreach(var item in result)
                    {
                        switch (item.Jenis)
                        {
                            case StatusKehadiran.Izin:
                                lap.Izin++;
                                break;
                            case StatusKehadiran.Sakit:
                                lap.Sakit++;
                                break;
                            case StatusKehadiran.Cuti:
                                lap.Cuti++;
                                break;
                            case StatusKehadiran.TugasBelajar:
                                lap.TugasBelajar++;
                                break;
                            case StatusKehadiran.DinasLuar:
                                lap.DinasLuar++;
                                break;
                            case StatusKehadiran.TugasKedinasan:
                                lap.TugasKedinasan++;
                                break;
                            default:
                                break;
                        }
                    }
                }
                return lap;
            }
        }

        public Laporan Perizinan(DateTime date,DateTime last, int PegawaiId)
        {

            using (var db = new OcphDbContext())
            {
                Laporan lap = new Laporan(1+(int)last.Subtract(date).TotalDays);
         
                for (DateTime tgl=date;tgl<=last;tgl=tgl.AddDays(1))
                {

                    if (tgl.DayOfWeek != DayOfWeek.Sunday || tgl.DayOfWeek != DayOfWeek.Saturday)
                    {
                        var result = db.Perizinan.Where(O => O.PegawaiId == PegawaiId && O.Mulai <= tgl && O.Selesai >= tgl).FirstOrDefault();
                        if (result != null)
                        {
                            switch (result.Jenis)
                            {
                                case StatusKehadiran.Izin:
                                    lap.Izin++;
                                    break;
                                case StatusKehadiran.Sakit:
                                    lap.Sakit++;
                                    break;
                                case StatusKehadiran.Cuti:
                                    lap.Cuti++;
                                    break;
                                case StatusKehadiran.TugasBelajar:
                                    lap.TugasBelajar++;
                                    break;
                                case StatusKehadiran.DinasLuar:
                                    lap.DinasLuar++;
                                    break;
                                case StatusKehadiran.TugasKedinasan:
                                    lap.TugasKedinasan++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                return lap;
            }
        }
    }
}