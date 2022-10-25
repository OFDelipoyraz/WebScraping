using Microsoft.AspNetCore.Mvc;

namespace Scraping.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
