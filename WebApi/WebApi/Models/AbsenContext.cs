using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.DataModels;
using WebApi.Controllers;

namespace WebApi.Models
{
    public class AbsenContext
    {
        private absen value;
        public AbsenContext(absen value)
        {
            this.value = value;
            DoAbsen(value);
        }

        public perizinan Perizinan { get; private set; }
        public bool Success { get; private set; }

        private void DoAbsen(absen value)
        {
            if (!NotHavePerizinan())
            {
                var setting = GetPengaturan();
                if(setting!=null && NotHollyDay())
                {

                    var todayController = new AbsenTodayController();
                    var today= todayController.Get(value.PegawaiId);
                    if (UserCanAbsen(setting,today))
                    {
                        using (var db = new OcphDbContext())
                        {
                            if (GetAbsenStatus(setting) == AbsenStatus.Masuk)
                            {
                                value.Status = Library.StatusKehadiran.Hadir;
                                value.Id = db.Absen.InsertAndGetLastID(value);
                                if (value.Id<0)
                                {
                                    throw new SystemException("Terjadi Kesalahan, Hubungi Administrator");
                                }
                            }else if(GetAbsenStatus(setting)==AbsenStatus.Pulang)
                            {
                                today.JamPulang = DateTime.Now.TimeOfDay;
                                if(!db.Absen.Update(O=>new { O.JamPulang},today,O=>O.Id==today.Id))
                                {
                                    throw new SystemException("Terjadi Kesalahan, Hubungi Administrator");
                                }
                            }
                        }
                        this.Success = true;
                    }else
                    {
                        throw new SystemException("Terjadi Kesalahan, Hubungi Administrator");
                    }
                }


            }
            else
                throw new SystemException("Anda Sedang  " + Perizinan.Jenis);
        }

        private bool UserCanAbsen(pengaturan setting,absen data)
        {
          var absenStatus=  GetAbsenStatus(setting);

            if (absenStatus == AbsenStatus.Masuk && data == null)
            {
                return true;
            }
            else if (absenStatus == AbsenStatus.Terlambat && data == null)
            {
                throw new SystemException("Maaf Anda Terlambat");

            }
            else if (absenStatus == AbsenStatus.Masuk && data != null)
            {
                throw new SystemException("Maaf Anda Sudah Absen Masuk");
            }
            else if (absenStatus == AbsenStatus.Terlambat && data != null)
            {
                throw new SystemException("Maaf Belum Saatnya Jam Pulang");
            }
            else if (absenStatus == AbsenStatus.Pulang && data == null)
            {
                throw new SystemException("Maaf Anda Tidak Tidak Masuk Hari Ini ");
            }
            else if (absenStatus == AbsenStatus.Pulang && data != null && data.JamPulang != new TimeSpan())
            {
                throw new SystemException("Maaf Anda Sudah Absen Jam Pulang ");
            }
            else
                return true;

            
        }

        private AbsenStatus GetAbsenStatus(pengaturan setting)
        {
            if (setting.JamMasuk.Add(TimeSpan.FromMinutes(setting.KonpensasiTerlambat)) < DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay < setting.JamPulang)
            {
                return AbsenStatus.Terlambat;
            }
            else if ((setting.JamMasuk.Add(TimeSpan.FromMinutes(setting.KonpensasiTerlambat)) > DateTime.Now.TimeOfDay))
            {
                return AbsenStatus.Masuk;
            }
            else
                return AbsenStatus.Pulang;
        }

        private bool NotHollyDay()
        {
          if(true)
            {
                return true;
            }else
            {
                throw new SystemException("Hari Ini Libur");
            }
        }

        private pengaturan GetPengaturan()
        {
            using (var db = new OcphDbContext())
            {
              var pengaturan =   db.Setting.Select().FirstOrDefault();
                if (pengaturan != null)
                    return pengaturan;
                else
                    throw new SystemException("Pengaturan Belum Anda, Tambahkan Pengaturan Baru");
            }
        }

        private bool NotHavePerizinan()
        {

            using (var db = new OcphDbContext())
            {
              this.Perizinan = db.Perizinan.Where(O => O.PegawaiId == value.PegawaiId && O.Mulai<=value.Tanggal && O.Selesai >= value.Tanggal).FirstOrDefault();
                return Perizinan == null ? false : true;
            }
        }
    }

    public enum AbsenStatus
    {
        Masuk,Pulang,
        Terlambat
    }

}