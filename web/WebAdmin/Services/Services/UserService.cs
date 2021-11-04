using LiteDB;

using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using WebAdmin.Models.Account;
using WebAdmin.Models.Projects;
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
        #region Login
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
            return loginResult;
        }
        public LoginResult GenerateToken(User user)
        {
            var claimIdentity = user.GenerateClaim();
            var now = System.DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                   issuer: State.ISSUER,
                   audience: State.AUDIENCE,
                   notBefore: now,
                   claims: claimIdentity.Claims,
                   expires: now.Add(TimeSpan.FromMinutes(State.LIFETIME)),
                   signingCredentials: new SigningCredentials(State.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            user.RefreshToken = Guid.NewGuid().ToString();
            _users.Update(user);
            return new LoginResult()
            {
                AccessToken = encodedJwt,
                RefreshToken = user.RefreshToken,
                Error = null,
                HttpStatus = 200,
                Success = true
            };

        }
        #endregion
        #region Registration
        public RegisterResult RegisterUser(RegisterViewModel user)
        {
            RegisterResult result = new RegisterResult();
            var exist = _users.FindOne(m => m.UserName == user.UserName);
            if (exist == null)
            {
                return AddNewUser(user);

            }
            result.Success = false;
            return result;
        }
        private RegisterResult AddNewUser(RegisterViewModel model)
        {
            User user = new User();
            user.Id = ObjectId.NewObjectId().ToString();
            user.UserName = model.UserName;
            user.Password = model.Password.Hash();
            user.Email = model.Email;
            user.Projects = model.UserProjects ?? new List<UserProject>();
            _users.Insert(user);
            return new RegisterResult()
            {
                Success = true,
            };
        }

        public User GetUserByName(string name)
        {
            return _users.FindOne(m => m.UserName == name);
        }



        public void AddProjectUser(AddUserProject addUserProject)
        {
            var addUser = _users.FindById(addUserProject.UserId);
            var existProject = addUser.Projects.FirstOrDefault(m => m.Id == addUserProject.ProjectId);
            addUser.Projects.Add(existProject);
            _users.Update(addUser);
        }
        #endregion


        public void AddProjectUser(Project project)
        {
            var user = _users.FindById(project.UserId);
            if (user == null)
            {

            }
            user.Projects.Add(new UserProject() { Id = project.Id, ProjectName = project.Name });
            _users.Update(user);

        }

        public void RemoveProject(User user, string id)
        {
            var projects = user.Projects.Where(m => m.Id == id).ToList();
            foreach (var i in projects)
            {
                user.Projects.Remove(i);
            }
            _users.Update(user);
        }
    }
}
