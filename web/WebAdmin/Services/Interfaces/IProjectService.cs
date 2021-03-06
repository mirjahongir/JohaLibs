using System.Collections.Generic;
using System.Threading.Tasks;

using WebAdmin.Models.Account;
using WebAdmin.Models.Projects;
using WebAdmin.ViewModels.Project;
using WebAdmin.ViewModels.QueryModels;

namespace WebAdmin.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> Get(ModelQuery model, User user);
        Task<ProjectResult> AddNewProject(Project project);
        Task<ProjectResult> UpdateProject(Project project);
        Task<ProjectResult> DeleteProject(User user, string id);
    }
}
