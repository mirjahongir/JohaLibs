using JohaRepository.Models.ErrorModels;

using LiteDB;

using System;

namespace WebAdmin.Models.Projects
{
    public class Project
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }


    }
    public class ProjectError
    {
        [BsonId]
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public ErrorModal ErrorModel { get; set; }

    }
}
