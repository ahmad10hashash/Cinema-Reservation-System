using Microsoft.AspNetCore.Mvc;

namespace CRS.Areas.Administrator.Controllers
{
    public class okController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
