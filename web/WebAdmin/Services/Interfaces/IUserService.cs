using WebAdmin.Models.Account;
using WebAdmin.Models.Configs;
using WebAdmin.Models.Projects;
using WebAdmin.ViewModels.Account;

namespace WebAdmin.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult LoginUser(LoginViewModel model);
        RegisterResult RegisterUser(RegisterViewModel model);
        User GetUserByName(string name);
        void AddProjectUser(Project project);
        void AddProjectUser(AddUserProject addUserProject);
        void RemoveProject(User user, string id);
        User Get(string id);
    }
    public interface IConfigService
    {
        public Config GetConfig { get; set; }
    }
}
