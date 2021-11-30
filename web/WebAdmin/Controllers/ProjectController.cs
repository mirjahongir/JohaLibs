
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

using WebAdmin.Models.Projects;
using WebAdmin.Services.Interfaces;
using WebAdmin.ViewModels.Project;
using WebAdmin.ViewModels.QueryModels;
namespace WebAdmin.Controllers
{
    [Authorize]
    [Route("/apimate/[controller]/[action]")]
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectService _project;
        private readonly IUserService _user;
        public ProjectController(ILogger<ProjectController> logger,
            IProjectService project,
             IUserService user)
        {
            _project = project;
            _logger = logger;
            _user = user;
        }
        [HttpGet]
        public async Task<List<Project>> Get([FromQuery] ModelQuery model)
        {
            var user = _user.Get(model.UserId);
            return await _project.Get(model, user);
        }
        [HttpPost]
        public async Task<ProjectResult> Post([FromBody] Project project)
        {
            var result = await _project.AddNewProject(project);
            if (!result.IsSuccess)
            {
                Response.StatusCode = result.HttpStatus;
                return result;
            }
            _user.AddProjectUser(project);
            return result;
        }

        public async Task<ProjectResult> Put([FromBody] Project project)
        {
            var result = await _project.UpdateProject(project);
            if (!result.IsSuccess)
            {
                Response.StatusCode = result.HttpStatus;
            }
            return result;
        }
        public async Task<ProjectResult> Delete([FromQuery] string id)
        {

            var user = _user.GetUserByName(User.Identity.Name);
            var result = await _project.DeleteProject(user, id);
            _user.RemoveProject(user, id);
            return result;
        }
    }
}
