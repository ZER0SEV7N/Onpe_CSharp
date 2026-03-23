using Microsoft.AspNetCore.Mvc;

namespace Onpe.Controllers
{
    public class PresidencialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
