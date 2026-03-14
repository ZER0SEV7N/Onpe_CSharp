using Microsoft.AspNetCore.Mvc;
using Onpe.Models;
using System.Diagnostics;

namespace Onpe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
