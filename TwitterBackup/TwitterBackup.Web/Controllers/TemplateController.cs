namespace TwitterBackup.Web.Controllers
{
    using System.Web.Mvc;

    public class TemplateController : Controller
    {
        // GET: Return partial view of admin page
        public ActionResult Admin()
        {
            return PartialView("_Admin");
        }
    }
}