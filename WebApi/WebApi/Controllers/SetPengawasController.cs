using Library.DataModels;
using Microsoft.AspNet.Identity.Owin;
using MySql.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class SetPengawasController : ApiController
    {
        // GET: api/SetPengawas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SetPengawas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SetPengawas
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SetPengawas/5
        public async Task<HttpResponseMessage> Put(int id, [FromBody]pegawai value)
        {
            var role = "Pengawas";
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    if (value.Pengawas == false)
                    {
                        var isExis = await AppRoleManager.RoleExistsAsync(role);
                        if (!isExis)
                        {
                            var r = await AppRoleManager.CreateAsync(new IdentityRole(role) { Name = role });
                            if (!r.Succeeded)
                            {
                                throw new System.Exception(string.Format("Role {0} Gagal Dibuat, Hubungi Administrator", role));
                            }
                        }

                       if( db.Pegawai.Update(O => new { O.Pengawas }, new pegawai { Pengawas = true }, O => O.Id == value.Id))
                        {
                            var roleResult = await UserManager.AddToRoleAsync(value.UserId, role);
                            if (!roleResult.Succeeded)
                            {
                                throw new System.Exception(string.Format("Gagal Menambahkan User Role"));
                            }else
                            {
                                trans.Commit();
                                value.Pengawas = true;
                                return Request.CreateResponse(HttpStatusCode.OK, value);
                            }
                        }else
                            throw new System.Exception(string.Format("Gagal Menambahkan User Role"));
                    }
                    else
                    {
                        if (db.Pegawai.Update(O => new { O.Pengawas }, new pegawai { Pengawas = false }, O => O.Id == value.Id))
                        {
                            var roleResult = await UserManager.RemoveFromRoleAsync(value.UserId, role);
                            if (!roleResult.Succeeded)
                            {
                                throw new System.Exception(string.Format("Gagal Menghapus User Role"));
                            }
                            else
                            {
                                trans.Commit();
                                value.Pengawas = false;
                                return Request.CreateResponse(HttpStatusCode.OK, value);
                            }
                        }else
                            throw new System.Exception(string.Format("Gagal Menghapus User Role"));
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }

        }

        // DELETE: api/SetPengawas/5
        public void Delete(int id)
        {
        }


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _appRoleManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _appRoleManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _appRoleManager = value;
            }
        }
    }
}
