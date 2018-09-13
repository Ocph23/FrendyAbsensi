using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library;

namespace WebApi.Controllers
{
    public class LaporanController : ApiController
    {
        // GET: api/Laporan
        [Route("api/Laporan/minggu/{date}")]
        public HttpResponseMessage GetMingguan(DateTime date)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var manage = new DateManage(date);
                    DateTime first = manage.FirstDayOfWeek;
                    DateTime last = manage.LastDayOfWeek;
                    Models.KehadiranContext context = new Models.KehadiranContext(first, last);
                    var result = db.Pegawai.Select().ToList();
                    if(result!=null)
                    {
                        foreach (var item in result)
                        {
                            var res = db.Absen.Where(x => x.PegawaiId == item.Id).Where(O => O.Tanggal.Day >= first.Day && O.Tanggal.Month >= first.Month && O.Tanggal.Year >= first.Year &&
                                 O.Tanggal.Day <= last.Day && O.Tanggal.Month <= last.Month && O.Tanggal.Year <= last.Year).ToList();

                            var lap = context.Perizinan(first, last, item.Id);
                            lap.Hadir = res.Where(O => O.Status == StatusKehadiran.Hadir).Count();
                            lap.Libur = context.TotalLibur;
                            Tuple<int,int,int> hasil = context.HitungTerlambat(res);
                            lap.Terlambat = hasil.Item1;
                            lap.CepatPulang = hasil.Item2;
                            lap.Lembur = hasil.Item3;
                            item.Sumarry = lap;
                        }
                      
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message), ex.Message);
            }
        }

        [Route("api/Laporan/bulan/{date}")]
        public HttpResponseMessage GetBulan(DateTime date)
        {
            var manage = new DateManage(date);
            DateTime first = manage.FirstDate;
            DateTime last = manage.LastDate;
           
            using (var db = new OcphDbContext())
            {
                var result = db.Pegawai.Select().ToList();
                foreach (var item in result)
                    item.Absens = db.Absen.Where(O => O.Tanggal >= first && O.Tanggal <= last && O.PegawaiId == item.Id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        // GET: api/Laporan/5
        [Route("api/Laporan/{date}")]
        public HttpResponseMessage Get(DateTime date)
        {
            try
            {
                Models.KehadiranContext context = new Models.KehadiranContext();
                if (context.IsHoliday(date))
                {
                    throw new SystemException(date.ToShortDateString() + " adalah hari libur");
                }

                using (var db = new OcphDbContext())
                {
                    var result = db.Pegawai.Select().ToList();
                    foreach (var item in result)
                    {
                        var res = db.Absen.Where(x => x.PegawaiId == item.Id)
                            .Where(O=> O.Tanggal.Day == date.Day && O.Tanggal.Month==date.Month&& O.Tanggal.Year==date.Year ).ToList();

                        var lap = context.Perizinan(date, item.Id);
                        lap.Hadir = res.Where(O => O.Status == StatusKehadiran.Hadir).Count();
                        lap.Libur = context.TotalLibur;
                        item.Sumarry = lap;


                    }




                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message), ex.Message);
            }
        }




        // GET: api/Laporan
        [Route("api/Laporan/minggu/{date}/{id}")]
        public HttpResponseMessage GetMingguanByPegawai(DateTime date, int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var manage = new DateManage(date);
                    DateTime first = manage.FirstDayOfWeek;
                    DateTime last = manage.LastDayOfWeek;
                    Models.KehadiranContext context = new Models.KehadiranContext(first, last);
                    var result = db.Pegawai.Where(O => O.Id == id);
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            var res = db.Absen.Where(x => x.PegawaiId == item.Id).Where(O => O.Tanggal.Day >= first.Day && O.Tanggal.Month >= first.Month && O.Tanggal.Year >= first.Year &&
                                 O.Tanggal.Day <= last.Day && O.Tanggal.Month <= last.Month && O.Tanggal.Year <= last.Year).ToList();

                            var lap = context.Perizinan(first, last, item.Id);
                            lap.Hadir = res.Where(O => O.Status == StatusKehadiran.Hadir).Count();
                            lap.Libur = context.TotalLibur;
                            Tuple<int, int, int> hasil = context.HitungTerlambat(res);
                            lap.Terlambat = hasil.Item1;
                            lap.CepatPulang = hasil.Item2;
                            lap.Lembur = hasil.Item3;
                            item.Sumarry = lap;
                        }

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, result.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message), ex.Message);
            }
        }

        [Route("api/Laporan/bulan/{date}/{id}")]
        public HttpResponseMessage GetBulanByPegawaiId(DateTime date,int id)
        {
            var manage = new DateManage(date);
            DateTime first = manage.FirstDate;
            DateTime last = manage.LastDate;

            using (var db = new OcphDbContext())
            {
                var result = db.Pegawai.Where(O=>O.Id==id).ToList();
                foreach (var item in result)
                    item.Absens = db.Absen.Where(O => O.Tanggal >= first && O.Tanggal <= last && O.PegawaiId == item.Id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result.FirstOrDefault());
            }
        }

        // GET: api/Laporan/5
        [Route("api/Laporan/{date}/{id}")]
        public HttpResponseMessage GetByPegawaiId(DateTime date,int id)
        {
            try
            {
                Models.KehadiranContext context = new Models.KehadiranContext();
                if (context.IsHoliday(date))
                {
                    throw new SystemException(date.ToShortDateString() + " adalah hari libur");
                }

                using (var db = new OcphDbContext())
                {
                    var result = db.Pegawai.Where(O=>O.Id==id).ToList();
                    foreach (var item in result)
                    {
                        var res = db.Absen.Where(x => x.PegawaiId == item.Id)
                            .Where(O => O.Tanggal.Day == date.Day && O.Tanggal.Month == date.Month && O.Tanggal.Year == date.Year).ToList();

                        var lap = context.Perizinan(date, item.Id);
                        lap.Hadir = res.Where(O => O.Status == StatusKehadiran.Hadir).Count();
                        lap.Libur = context.TotalLibur;
                        item.Sumarry = lap;


                    }




                    return Request.CreateResponse(HttpStatusCode.OK, result.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message), ex.Message);
            }
        }

















        // POST: api/Laporan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Laporan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Laporan/5
        public void Delete(int id)
        {
        }
    }
}
