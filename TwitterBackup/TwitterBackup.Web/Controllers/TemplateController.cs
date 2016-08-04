using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TwitterBackup.Web.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Admin
        public ActionResult Admin()
        {
            return PartialView("_Admin");
        }
    }
}