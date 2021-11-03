using AspNetCoreResult.ResponseCoreResult;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WebAdmin.Models.Projects;
using WebAdmin.Services.Interfaces;
using WebAdmin.ViewModels.ProjectError;
using WebAdmin.ViewModels.QueryModels;
namespace WebAdmin.Controllers
{
    [Authorize]
    [Route("/apimate/[controller]/[action]")]
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
                return ext;
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
