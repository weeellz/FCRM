using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FSRM.Models;
using FSRM.Services;
using FSRM.Results;
namespace FSRM.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private AccountService accountService = new AccountService();
        [Route("register")]
        [HttpPost]
        public Result Register(RegisterModel model)
        {
            return accountService.Register(model);
        }

        [Route("login")]
        [HttpPost]
        public Result Login(LoginModel model)
        { 
            return accountService.Login(model);
        }

        [Route("myprofile")]
        [HttpGet]
        public ProfileResult MyProfile()
        {
            return accountService.CurrentUserProfile();
        }
    }
}
