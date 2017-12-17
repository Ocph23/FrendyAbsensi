using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class BidangController : ApiController
    {
        // GET: api/Pegawai
        public IEnumerable<bidang> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Bidang.Select()
                             select a;
                return result.ToList();
            }
        }

        // GET: api/Pegawai/5
        public bidang Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Bidang.Where(O => O.Id == id)
                             select a;
                return result.FirstOrDefault();
            }
        }

        // POST: api/Pegawai
        public HttpResponseMessage Post([FromBody]bidang value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                        value.Id = db.Bidang.InsertAndGetLastID(value);
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
        public HttpResponseMessage Put(int id, [FromBody]bidang value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                        var isUpdate = db.Bidang.Update(O => new {O.Keterangan,O.Nama },value, O => O.Id == value.Id);
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
                        var isDelete = db.Bidang.Delete(O => O.Id == id);
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
