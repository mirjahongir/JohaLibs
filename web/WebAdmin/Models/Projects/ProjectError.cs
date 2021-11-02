using JohaRepository.Models.ErrorModels;

using LiteDB;

namespace WebAdmin.Models.Projects
{
    public class ProjectError
    {
        [BsonId]
        public string Id { get; set; }

        public string ProjectId { get; set; }
        public ErrorModal ErrorModel { get; set; }

    }
}
