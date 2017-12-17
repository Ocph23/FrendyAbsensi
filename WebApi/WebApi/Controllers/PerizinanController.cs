using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PerizinanController : ApiController
    {
        // GET: api/Pegawai
        public IEnumerable<perizinan> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Perizinan.Select()
                             select a;
                return result.ToList();
            }
        }

        // GET: api/Pegawai/5
        public perizinan Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Perizinan.Where(O => O.Id == id)
                             select a;
                return result.FirstOrDefault();
            }
        }

        // POST: api/Pegawai
        public HttpResponseMessage Post([FromBody]perizinan value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                        value.Id = db.Perizinan.InsertAndGetLastID(value);
                        if (value.Id > 0)
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

        // PUT: api/Pegawai/5
        public HttpResponseMessage Put(int id, [FromBody]perizinan value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                        var isUpdate = db.Perizinan.Update(O => new { O.Catatan,O.Jenis,O.Mulai,O.PegawaiId,O.Selesai,O.Status }, value, O => O.Id == value.Id);
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
                        var isDelete = db.Perizinan.Delete(O => O.Id == id);
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
