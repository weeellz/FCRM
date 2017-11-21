using FCRM.Web.Models;
using FCRM.Web.Results;
using FCRM.Web.Services;
using System;
using System.Web.Http;

namespace FCRM.Web.Controllers
{
    public class AccountApiController : ApiController
    {
        private UserService _userService = null;

        public AccountApiController() : this(new UserService()) { }

        public AccountApiController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("api/{key}/register")]
        public Result Register(Guid key, RegisterViewModel model)
        {
            return _userService.RegisterUser(key, model.UserName, model.Password);
        }
    }
}
