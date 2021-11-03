using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.ViewModels.ProjectError
{
    public class ProjectErrorResult
    {
        public bool IsSuccess { get; set; }
        public WebAdmin.Models.Projects.ProjectError ProjectError { get; set; }
    }
  
}
