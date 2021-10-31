using JohaRepository.Attributes.Auth;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAdmin.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name is Rquire")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Require")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is Require")]
        public string Email { get; set; }

        public List<Models.Account.UserProject> UserProjects { get; set; }
    }
    [AuthModel]
    public class AddUserProject
    {
        [JwtProperty("id")]
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public string AddUserId { get; set; }
    }
}
