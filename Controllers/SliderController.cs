using CRS.Data;
using Microsoft.AspNetCore.Mvc;

namespace CRS.Controllers
{
    public class SliderController : Controller
    {
        private CRSDbContext db;
        public SliderController(CRSDbContext _db) { db = _db; }
        public IActionResult Index()
        {
            
            return View(db.Movies);
        }
    }
}
