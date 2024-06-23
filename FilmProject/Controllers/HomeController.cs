using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilmProject.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

    
    }
}
