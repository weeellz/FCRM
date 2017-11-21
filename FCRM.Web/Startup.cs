using FCRM.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(FCRM.Web.Startup))]
namespace FCRM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer<SiteContext>(new CreateDatabaseIfNotExists<SiteContext>());
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SiteContext()));

            // создаем две роли
            var role1 = new IdentityRole { Name = "owner" };
            var role2 = new IdentityRole { Name = "performer" };
            var role3 = new IdentityRole { Name = "manager" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            ConfigureAuth(app);
        }
    }
}
