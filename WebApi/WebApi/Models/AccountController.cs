using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using System.Linq;
using MySql.AspNet.Identity;
using WebApi;
using WebApi.Models;

namespace PenilaianPegawaiWeb.Apis
{
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _appRoleManager;

        // GET: api/Account
        // GET: api/Account


        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public AccountController()
        {
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

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [AllowAnonymous]

        [HttpGet]
        public async Task<HttpResponseMessage> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
            }

            var c = HttpUtility.UrlDecode(code);
            var result = await UserManager.ConfirmEmailAsync(userId, c);
            return Request.CreateResponse(result.Succeeded ? "ConfirmEmail" : "Error");
        }



        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetPejabatPenilaiProfile()
        {
            var ui = User.Identity.Name;
           // pegawai result;
            try
            {
               
                    throw new SystemException("Anda Tidak Memiliki Hak Akses");
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        // POST: api/Account
     
        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
