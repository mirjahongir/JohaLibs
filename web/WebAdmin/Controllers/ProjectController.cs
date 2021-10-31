using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAdmin.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Models.Projects;
using WebAdmin.ViewModels.QueryModels;
using Microsoft.AspNetCore.Authorization;
using WebAdmin.ViewModels.Project;
namespace WebAdmin.Controllers
{
    [Authorize]
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
            return await _project.Get(model);
        }
        [HttpPost]
        public async Task<ProjectResult> Post([FromBody] Project project)
        {
            var result = await _project.AddNewProject(project);
            _user.AddProjectUser(project);
            return result;
        }
        
        public async Task<ProjectResult> Put([FromBody] Project project)
        {
            return await _project.UpdateProject(project);
        }
        public async Task<ProjectResult> Delete([FromQuery] string id)
        {

            var user = _user.GetUserByName(User.Identity.Name);
            return await _project.DeleteProject(user, id);
        }
    }
}
