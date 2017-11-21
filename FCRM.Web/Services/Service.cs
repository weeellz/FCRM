using FCRM.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;

namespace FCRM.Web.Services
{
    public abstract class Service
    {
        private User user = null;

        protected Guid CurrentCompanyId(SiteContext db)
        {
            Guid companyId = Guid.Empty;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                companyId = (
                            from u in db.Users
                            where u.UserName == HttpContext.Current.User.Identity.Name
                            select u.Company.Id
                            ).FirstOrDefault();
            }

            if(companyId == Guid.Empty)
                throw new UnauthorizedAccessException("Пользователь неавторизован");

            return companyId;
        }

        protected virtual User CurrentUser(SiteContext db)
        {
            if (user == null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                user = db.Users.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
            }

            if (user == null)
                throw new UnauthorizedAccessException("Пользователь неавторизован");

            return user;
        }

        protected virtual T CallUserManager<T>(SiteContext db, Func<UserManager<IdentityUser>, T> invoke)
        {
            T result = default(T);
            using (var store = new UserStore<IdentityUser>(db))
            {
                using (var manager = new UserManager<IdentityUser>(store))
                {
                    result = invoke(manager);
                }
            }
            return result;
        }
    }
}