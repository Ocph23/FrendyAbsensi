using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PegawaiController : ApiController
    {
        // GET: api/Pegawai
        public IEnumerable<pegawai> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Pegawai.Select()
                             select a;
                return result.ToList();
            }
        }

        // GET: api/Pegawai/5
        public pegawai Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Pegawai.Where(O => O.Id == id)
                             select a;
                return result.FirstOrDefault();
            }
        }

        // POST: api/Pegawai
        public HttpResponseMessage Post([FromBody]pegawai value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                        value.Id = db.Pegawai.InsertAndGetLastID(value);
                        if(value.Id>0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        }else
                        {
                            throw new SystemException("Data tidak tersimpan");
                        }
                    }
                }
                else
                {
                    throw new SystemException("Data Tidak Valid");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
            }
        }

        // PUT: api/Pegawai/5
        public HttpResponseMessage Put(int id, [FromBody]pegawai value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                       var isUpdate = db.Pegawai.Update(O=>new { O.Alamat,O.IdBidang,O.IdJabatan,O.JenisKelamin,O.Nama,O.NIP,O.TanggalLahir,O.Telepon,O.TempatLahir,O.UserId},
                            value,O=>O.Id==value.Id );
                        if (isUpdate)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        }
                        else
                        {
                            throw new SystemException("Data tidak tersimpan");
                        }
                    }
                }
                else
                {
                    throw new SystemException("Data Tidak Valid");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
            }
        }

        // DELETE: api/Pegawai/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                        var isDelete = db.Pegawai.Delete(O => O.Id == id);
                        if (isDelete)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "Data berhasil dihapus");
                        }
                        else
                        {
                            throw new SystemException("Data tidak tersimpan");
                        }
                    }
                }
                else
                {
                    throw new SystemException("Data Tidak Valid");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message);
            }
        }
    }
}
