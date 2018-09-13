using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PengaturanController : ApiController
    {
        // GET: api/Pengaturan
        public HttpResponseMessage Get()
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    List<pengaturan> list = new List<pengaturan>();
                    var result = db.Setting.Select().ToList();
                   return Request.CreateResponse(HttpStatusCode.OK,result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // GET: api/Pengaturan/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, db.Setting.Where(O=>O.Id==id).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Pengaturan
        public HttpResponseMessage Post([FromBody]pengaturan value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    value.Id = db.Setting.InsertAndGetLastID(value);
                    if(value.Id>0)
                        return Request.CreateResponse(HttpStatusCode.Accepted, value);
                    else
                    {
                        throw new SystemException("Data Tidak Tersimpan");
                    }
                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }

    }
}
