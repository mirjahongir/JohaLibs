using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Models.Projects;
using WebAdmin.ViewModels.ProjectError;
using WebAdmin.ViewModels.QueryModels;

namespace WebAdmin.Services.Interfaces
{
    public interface IProjectErrorService
    {
        Task<List<ProjectError>> Query(ModelQuery model);
        Task<ProjectErrorResult> Post(ProjectError model);
        Task<ProjectErrorResult> Put(ProjectError model);
        Task<ProjectErrorResult> Delete(string projectId, string id);
    }
}
