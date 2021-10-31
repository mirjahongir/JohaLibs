using Microsoft.AspNetCore.Mvc;
using WebAdmin.Services.Interfaces;
using System.Diagnostics;

using WebAdmin.ViewModels.Account;
namespace WebAdmin.Controllers
{
    public class AccountController : Controller
    {
        IUserService _user;
        public AccountController(IUserService user)
        {
            _user = user;
        }
        public LoginResult Login([FromBody] LoginViewModel model)
        {
            _user.LoginUser(model);
        }

    }

    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
