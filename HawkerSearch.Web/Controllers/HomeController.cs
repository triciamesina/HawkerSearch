using Microsoft.AspNetCore.Mvc;

namespace HawkerSearch.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Search");
        }
    }
}
