using LiteDB;

namespace WebAdmin.Models.Configs
{
    public class Config
    {
        [BsonId]
        public string Id { get; set; }
        public bool RegisterEnabled { get; set; }
    }
}
