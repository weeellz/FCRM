using FSRM.Models;
using FSRM.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace FSRM.Services
{
    public class AccountService
    {
        private SiteContext db = new SiteContext();
        private User authUser;
        public Result Register(RegisterModel model)
        {
            if(WebSecurity.UserExists(model.UserName))
            {
                return new Result(string.Format("Пользователь с именем {0} существует", model.UserName));
            }
            WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
            return Result.Empty;
        }
        public Result Login(LoginModel model)
        {
            WebSecurity.Login(model.UserName, model.Password, true);
            return Result.Empty;
        }
        public Result Logout()
        {
            WebSecurity.Logout();
            return Result.Empty;
        }
        public Result ChangePassword(ChangePasswordModel model)
        {
            WebSecurity.ChangePassword(WebSecurity.CurrentUserName, model.OldPassword, model.NewPassword);
            return Result.Empty;
        }
        public ProfileResult GetProfile(int userId)
        {
            var user = db.Users.Find(userId);
            if (user == null)
                return new ProfileResult("Пользователь не найден");
            return new ProfileResult(user);
        }
        public ProfileResult CurrentUserProfile()
        {
            if (!WebSecurity.IsAuthenticated)
                return new ProfileResult("Вы не авторизированны");
            return GetProfile(WebSecurity.CurrentUserId);
        }
    }
}