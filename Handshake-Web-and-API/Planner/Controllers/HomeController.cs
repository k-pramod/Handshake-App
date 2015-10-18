using System.Web.Mvc;

namespace Planner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*if (Session["EmailAddress"].ToString() != "")
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.EmailAddress = Session["EmailAddress"] as string;
                return View();
                //return RedirectToAction("Secured");
            }
            if (Session["LoginFailed"].ToString() != "")
            {
                ViewBag.LoginFailed = true;
            }
            ViewBag.IsLoggedIn = false;*/
            return View();
        }
    }
}