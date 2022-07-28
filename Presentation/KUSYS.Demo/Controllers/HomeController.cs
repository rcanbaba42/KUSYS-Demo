using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
