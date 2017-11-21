using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FCRM.Web.Services;

namespace FCRM.Web.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="owner")]
        public ActionResult Manage()
        {
            return View();
        }
        public ActionResult ManagerMain()
        {
            return View();
        }
        public ActionResult Tasks()
        {
            return View();
        }
    }
}
