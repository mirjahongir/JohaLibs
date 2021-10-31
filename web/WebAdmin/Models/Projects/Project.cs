using JohaRepository.Attributes.Auth;
using JohaRepository.Models.ErrorModels;
using LiteDB;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAdmin.Models.Projects
{
    [AuthModel]
    public class Project
    {
        [BsonId]
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        [JwtProperty("id")]
        public string UserId { get; set; }

    }
    public class ProjectError
    {
        [BsonId]
        public string Id { get; set; }

        public string ProjectId { get; set; }
        public ErrorModal ErrorModel { get; set; }

    }
}
