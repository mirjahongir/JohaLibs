using LiteDB;

namespace WebAdmin.Models.Account
{
    public class User
    {
        [BsonId]
        public string Id {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public string UserName { get; set; }
        public System.Collections.Generic.List<UserProject> Projects { get; set; }
}
public class UserProject
{
    public string Id { get; set; }
    public string ProjectName { get; set; }
}
}
