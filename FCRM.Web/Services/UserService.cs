using FCRM.Web.Models;
using FCRM.Web.Results;
using Microsoft.AspNet.Identity;
using System;

namespace FCRM.Web.Services
{
    public class UserService : Service
    {
        public Result RegisterUser(Guid companyId, string userName, string password)
        {
            using (var db = new SiteContext())
            {
                return CallUserManager<Result>(db, userManager =>
                {
                    var company = db.Companies.Find(companyId);
                    if (company == null)
                    {
                        return new Result("Компания не найдена");
                    }
                    var user = new User
                    {
                        Name = userName,
                        UserName = userName,
                        Company = company
                    };
                    var result = userManager.Create(user, password);

                    if (result.Succeeded)
                    {
                        return Result.Empty;
                    }
                    else
                    {
                        return new Result(string.Join(", ", result.Errors));
                    }
                });
            }
            return new Result("Регистрация не удалась");
        }
    }
}