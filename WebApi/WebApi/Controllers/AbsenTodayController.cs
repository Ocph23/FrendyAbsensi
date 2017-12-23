using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AbsenTodayController : ApiController
    {
        // GET: api/AbsenToday
        public IEnumerable<pegawai> Get()
        {
            
            using (var db = new OcphDbContext())
            {
                DateTime tanggal = DateTime.Now;
                var result = from b in db.Pegawai.Select()
                             join c in db.Bidang.Select() on b.IdBidang equals c.Id
                             join d in db.Jabatan.Select() on b.IdJabatan equals d.Id
                             join a in db.Absen.Where(O => O.Tanggal.Year==tanggal.Year ) on b.Id equals a.PegawaiId  into g
                             from ab in g.DefaultIfEmpty()
                             select new pegawai
                             {
                                 Id = b.Id, Alamat=b.Alamat,  IdBidang =b.IdBidang, IdJabatan=b.IdJabatan, JenisKelamin=b.JenisKelamin, Nama=b.Nama, NIP=b.NIP, TanggalLahir=b.TanggalLahir,
                                  Telepon=b.Telepon, TempatLahir=b.TempatLahir, UserId=b.UserId, Bidang =c, Jabatan=d, AbsenToday=ab
                             };
                return result.ToList();
            }
        }

        // GET: api/AbsenToday/5
        public absen Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Absen.Where(O => O.Tanggal == DateTime.Now && O.PegawaiId == id)
                             join b in db.Pegawai.Select() on a.PegawaiId equals b.Id
                             select new absen
                             {
                                 Id = a.Id,
                                 JamMasuk = a.JamMasuk,
                                 JamPulang = a.JamPulang,
                                 PegawaiId = a.PegawaiId,
                                 PengaturanId = a.PengaturanId,
                                 PerizinanId = a.PerizinanId,
                                 Status = a.Status,
                                 Tanggal = a.Tanggal,
                                 Pegawai = b
                             };
                return result.FirstOrDefault();
            }
          
        }

        // POST: api/AbsenToday
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AbsenToday/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AbsenToday/5
        public void Delete(int id)
        {
        }
    }
}
