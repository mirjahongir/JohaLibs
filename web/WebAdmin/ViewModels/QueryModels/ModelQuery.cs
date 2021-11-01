using JohaRepository.Attributes.Auth;

using System.Collections.Generic;

namespace WebAdmin.ViewModels.QueryModels
{
    [AuthModel]
    public class ModelQuery
    {
        
        public string Id { get; set; }
        public string Name { get; set; }

        public int? PageNumber { get; set; } = 0;
        public int? PageSize { get; set; }
        [JwtProperty("id")]
        public string UserId { get; set; }
        [JwtProperty("project")]
        public List<string> UserProjects { get; set; }
    }
}
