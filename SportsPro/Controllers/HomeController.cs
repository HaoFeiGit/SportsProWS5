using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult List()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}