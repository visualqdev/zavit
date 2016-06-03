using System.Web.Mvc;

namespace zavit.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}