using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using WebMatrix.WebData;
using FSRM.Models;

namespace FSRM
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Код, выполняемый при запуске приложения
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SiteContext db = new SiteContext();
            Order order = new Order();
            order.Company = "kek";
            order.Email = "kek";
            order.Information = "kek";
            order.Name = "kek";
            order.Phone = "kek";
            order.Price = 123123123D;
            db.Orders.Add(order);
            db.SaveChanges();
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "UserName", true);
        }
    }
}