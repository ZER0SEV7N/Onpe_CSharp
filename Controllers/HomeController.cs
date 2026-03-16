using Microsoft.AspNetCore.Mvc;

namespace Onpe.Controllers
{
    //Controlador para manejar la vista principal del sitio web.
    public class HomeController : Controller
    {
        //GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
