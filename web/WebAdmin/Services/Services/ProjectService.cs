using LiteDB;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebAdmin.Models.Account;
using WebAdmin.Models.Projects;
using WebAdmin.Services.Interfaces;
using WebAdmin.ViewModels.Project;
using WebAdmin.ViewModels.QueryModels;

namespace WebAdmin.Services.Services
{
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

        public async Task<List<Project>> Get(ModelQuery model)
        {
            var projectList = _projects.Find(m => model.UserProjects.Contains(m.Id)).ToList();
            return projectList;

        }

        public async Task<ProjectResult> UpdateProject(Project project)
        {
            _projects.Update(project);
            return new ProjectResult() { HttpStatus = 200, IsSuccess = true, Project = project };
        }
    }
}
