using LiteDB;

using WebAdmin.Models.Account;
using WebAdmin.Services.Interfaces;
using WebAdmin.ViewModels.Account;

namespace WebAdmin.Services.Services
{
    public class UserService : IUserService
    {
        ILiteCollection<User> _users;
        public UserService(ILiteDatabase _db)
        {
            _users = _db.GetCollection<User>();
        }
        public LoginResult LoginUser(LoginViewModel model)
        {
            LoginResult loginResult = new LoginResult();
            var user = _users.FindOne(m => m.UserName == model.UserName);
            if (user == null)
            {
                loginResult.HttpStatus = 400;
                loginResult.Error = "User Not Found";
                return loginResult;
            }
            if (model.Password.Hash() == user.Password)
            {
                return GenerateToken(user);
            }
            loginResult.HttpStatus = 400;
            loginResult.Error = "Password incorect";
        }
        public LoginResult GenerateToken(User user)
        {
            
        }
    }
}
