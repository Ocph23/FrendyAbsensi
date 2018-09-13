using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LiburController : ApiController
    {
        // GET: api/Libur
        public IEnumerable<libur> Get()
        {
            using (var db = new OcphDbContext())
            {
                var a= db.Libur.Select();
                return a;
            }
        }

        // GET: api/Libur/5
        public HttpResponseMessage Get(int id)
        {
            try
            {

                using (var db = new OcphDbContext())
                {
                    var result= db.Libur.Where(O => O.Id == id).FirstOrDefault();
                    if (result != null)
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    else
                        throw new SystemException("Data Tidak Ditemukan");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,ex.Message), ex.Message);
            }
        }

        // POST: api/Libur
        public HttpResponseMessage Post([FromBody]libur value)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    using (var db = new OcphDbContext())
                    {
                        value.Id = db.Libur.InsertAndGetLastID(value);
                        if (value.Id > 0)
                        {

                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        }
                        else
                            throw new SystemException("Data Tidak Tersimpan");

                    }
                    
                }else
                {
                    throw new SystemException("Lengkapi Data Anda");
                }

            }
            catch (Exception ex)
            {

                return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message),ex.Message);
            }
        }

        // PUT: api/Libur/5
        public HttpResponseMessage Put(int id, [FromBody]libur value)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    using (var db = new OcphDbContext())
                    {
                        var isUpdated = db.Libur.Update(O => new { O.Keterangan, O.Tanggal }, value, O => O.Id == value.Id);
                        if (isUpdated)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        }
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }
                }
                else
                {
                    throw new SystemException("Lengkapi Data Anda");
                }

            }
            catch (Exception ex)
            {

                return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.BadGateway, ex.Message), ex.Message);
            }
        }

        // DELETE: api/Libur/5
        public HttpResponseMessage Delete(int id)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if (db.Libur.Delete(O => O.Id == id))
                        return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    else
                        throw new SystemException("Data tidak dapat dihapus");
                }
                catch (Exception ex)
                {
                    return new ErrorResponse(Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message), ex.Message);
                }
            }
        }
    }
}
