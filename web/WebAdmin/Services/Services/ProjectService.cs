using JohaRepository.Exceptions;

using LiteDB;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebAdmin.Models.Account;
using WebAdmin.Models.Projects;
using WebAdmin.Services.Interfaces;
using WebAdmin.ViewModels.Project;
using WebAdmin.ViewModels.ProjectError;
using WebAdmin.ViewModels.QueryModels;

namespace WebAdmin.Services.Services
{
    public class ProjectErrorService : IProjectErrorService
    {
        private readonly ILiteCollection<ProjectError> _error;
        public ProjectErrorService(ILiteDatabase db)
        {
            _error = db.GetCollection<ProjectError>();
        }
        public async Task<ProjectErrorResult> Delete(string projectId, string id)
        {
            var project = _error.FindOne(m => m.ProjectId == projectId && m.Id == id);
            if (project != null)
            {
                _error.Delete(id);
                return new ProjectErrorResult()
                {
                    IsSuccess = true
                };
            }
            return new ProjectErrorResult()
            {
                IsSuccess = false
            };
        }

        public async Task<ProjectErrorResult> Post(ProjectError model)
        {
            if (!CheckError(model))
            {
                return new ProjectErrorResult()
                {
                    IsSuccess = false
                };
            }

            model.Id = ObjectId.NewObjectId().ToString();
            _error.Insert(model);
            return new ProjectErrorResult()
            {
                IsSuccess = true,
                ProjectError = model
            };
        }
        public bool CheckError(ProjectError model)
        {
            if (string.IsNullOrEmpty(model.ProjectId))
            {
                return false;
            }
            return true;
        }

        public async Task<ProjectErrorResult> Put(ProjectError model)
        {
            if (!CheckError(model))
            {
                return new ProjectErrorResult() { IsSuccess = false };
            }
            _error.Update(model);
            return new ProjectErrorResult()
            {
                IsSuccess = true,
                ProjectError = model
            };
        }

        public async Task<List<ProjectError>> Query(ModelQuery model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                throw new RepoException("Project Id not Found");
            }
            var query = _error.Find(m => m.ProjectId == model.Id);
            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(m => m.ErrorModel.UzbText.Contains(model.Name));
            }
            return query.ToList();
        }
    }
    public class ProjectService : IProjectService
    {
        private readonly ILiteCollection<Project> _projects;
        public ProjectService(ILiteDatabase db)
        {
            _projects = db.GetCollection<Project>();
        }
        public async Task<ProjectResult> AddNewProject(Project project)
        {
            var exist = _projects.FindOne(m => m.Name == project.Name);
            if (exist != null)
            {
                return new ProjectResult
                {
                    Error = "Project Exist",
                    HttpStatus = 400,
                    IsSuccess = false,

                };
            }
            project.Id = ObjectId.NewObjectId().ToString();
            _projects.Insert(project);

            return new ProjectResult()
            {
                IsSuccess = true,
                HttpStatus = 200,
                Project = project
            };


        }

        public async Task<ProjectResult> DeleteProject(User user, string id)
        {
            if (user.Projects.Exists(m => m.Id == id))

            {
                _projects.Delete(id);
                return new ProjectResult() { HttpStatus = 200, IsSuccess = true };
            }
            return new ProjectResult()
            {
                HttpStatus = 400,
                IsSuccess = false,
                Error = "User not Permission error"
            };

        }

        public async Task<List<Project>> Get(ModelQuery model, User user)
        {
            var projecrsId = user.Projects.Select(m => m.Id).ToList();

            var projectList = _projects.Find(m => projecrsId.Contains(m.Id)).ToList();
            return projectList;

        }

        public async Task<ProjectResult> UpdateProject(Project project)
        {
            _projects.Update(project);
            return new ProjectResult() { HttpStatus = 200, IsSuccess = true, Project = project };
        }
    }
}
