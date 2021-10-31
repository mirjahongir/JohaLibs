using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
namespace WebAdmin.Controllers
{

    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
