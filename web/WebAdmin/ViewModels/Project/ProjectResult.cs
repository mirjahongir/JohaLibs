namespace WebAdmin.ViewModels.Project
{
    public class ProjectResult
    {
        public int HttpStatus { get; set; }
        public string Error { get; set; }
        public bool IsSuccess { get; set; }
        public WebAdmin.Models.Projects.Project Project { get; set; }
    }
}
