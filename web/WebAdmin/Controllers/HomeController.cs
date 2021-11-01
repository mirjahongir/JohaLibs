using Microsoft.AspNetCore.Mvc;
namespace WebAdmin.Controllers
{
    [Route("/apimate/[controller]/[action]")]
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
