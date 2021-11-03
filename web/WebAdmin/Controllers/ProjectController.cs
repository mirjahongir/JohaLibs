using AspNetCoreResult.ResponseCoreResult;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WebAdmin.Models.Projects;
using WebAdmin.Services.Interfaces;
using WebAdmin.ViewModels.Project;
using WebAdmin.ViewModels.ProjectError;
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
            return await _project.Get(model);
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
    public class ProjectErrorController : Controller
    {
        IProjectErrorService _projectError;
        public ProjectErrorController(IProjectErrorService projectError)
        {
            _projectError = projectError;

        }
        [HttpGet]
        public async Task<CoreResult<List<ProjectError>>> Get([FromQuery] ModelQuery model)
        {
            try
            {
                return await _projectError.Query(model);
            }
            catch (Exception ext)
            {

            }
        }
        [HttpPost]
        public async Task<CoreResult<ProjectErrorResult>> Post([FromBody] ProjectError model)
        {
            return await _projectError.Post(model);
        }
        [HttpPut]
        public async Task<CoreResult<ProjectErrorResult>> Put([FromBody] ProjectError model)
        {
            return await _projectError.Put(model);
        }
        [HttpDelete]
        public async Task<CoreResult<ProjectErrorResult>> Delete(string projectId, string id)
        {
            return await _projectError.Delete(projectId, id);
        }
    }
}
