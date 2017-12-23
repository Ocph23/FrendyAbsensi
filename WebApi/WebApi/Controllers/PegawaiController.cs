using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using WebApi.Models;
using Library.DataModels;
using System.Web;
using System.Threading.Tasks;
using MySql.AspNet.Identity;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    public class PegawaiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _appRoleManager;

        // GET: api/Pegawai
        public IEnumerable<pegawai> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Pegawai.Select()
                             join b in db.Bidang.Select() on a.IdBidang equals b.Id
                             join c in db.Jabatan.Select() on a.IdJabatan equals c.Id
                             select new pegawai { Alamat=a.Alamat, Id=a.Id, IdBidang=a.IdBidang, IdJabatan=a.IdJabatan, JenisKelamin=a.JenisKelamin, Nama=a.Nama,
                              NIP=a.NIP, TanggalLahir=a.TanggalLahir, Telepon=a.Telepon, TempatLahir=a.TempatLahir, UserId=a.UserId,Bidang=b,Jabatan=c};
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
        public async Task<HttpResponseMessage> Post([FromBody]pegawai value)
        {
            RegisterViewModel model = new RegisterViewModel();
            model.Email = value.Email;
            model.Password = "User@123";

            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    string role = "Pegawai";
                    if (result.Succeeded)
                    {
                        var isExis = await AppRoleManager.RoleExistsAsync(role);
                        if (!isExis)
                        {
                            var r = await AppRoleManager.CreateAsync(new IdentityRole(role) { Name = role });
                            if (r.Succeeded)
                            {
                                var roleResult = await UserManager.AddToRoleAsync(user.Id,role);
                                if (!roleResult.Succeeded)
                                {
                                    throw new System.Exception(string.Format("Gagal Menambahkan User Role"));
                                }
                            }
                            else
                            {
                                throw new System.Exception(string.Format("Role {0} Gagal Dibuat, Hubungi Administrator", role));
                            }
                        }

                        using (var db = new OcphDbContext())
                        {
                            value.UserId = user.Id;
                            value.Id = db.Pegawai.InsertAndGetLastID(value);
                            string c = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            string code = HttpUtility.UrlEncode(c);
                            var callbackUrl = Url.Link("DefaultApi", new { controller = "Account/ConfirmEmail", userId = user.Id, code = code });
                            await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                            return Request.CreateResponse(HttpStatusCode.OK,value);
                        }
                    }
                    else
                        throw new SystemException("Data Tidak berhasil ditambah");
                }
                else
                {
                    throw new SystemException("Data Tidak Valid");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message, ex);
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
