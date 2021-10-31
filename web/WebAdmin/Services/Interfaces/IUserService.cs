using WebAdmin.ViewModels.Account;

namespace WebAdmin.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult LoginUser(LoginViewModel model);
    }
}
