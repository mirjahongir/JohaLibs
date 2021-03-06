using Microsoft.AspNetCore.Mvc;

using System;

using WebAdmin.Services.Interfaces;
using WebAdmin.ViewModels.Account;
namespace WebAdmin.Controllers
{
    [Route("/apimate/[controller]/[action]")]
    public class AccountController : Controller
    {
        IUserService _user;
        IConfigService _config;
        public AccountController(IUserService user, IConfigService config)
        {
            _user = user;
            _config = config;
        }
        [HttpPost]
        public LoginResult Login([FromBody] LoginViewModel model)
        {
            try
            {

                var result = _user.LoginUser(model);
                if (result.HttpStatus > 200)
                    Response.StatusCode = result.HttpStatus;
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //  [AlloAnanimus]
        [HttpPost]
        public RegisterResult RegisterUser([FromBody] RegisterViewModel model)
        {
            if (_config.GetConfig.RegisterEnabled)
            {
                return _user.RegisterUser(model);
            }
            return new RegisterResult() { Success = false };

        }
        [HttpPost]
        public object AddProjectToUser([FromBody] AddUserProject model)
        {
            _user.AddProjectUser(model);
            return null;
        }

    }
}
