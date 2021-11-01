using LiteDB;
using System.Linq;
using WebAdmin.Models.Configs;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Services.Services
{
    public class ConfigService : IConfigService
    {
        ILiteCollection<Config> _configs;
        public ConfigService(ILiteDatabase _db)
        {
            _configs = _db.GetCollection<Config>();

        }
        Config config;
        public Config GetConfig
        {
            get
            {
                if (config == null)
                {
                    config = _configs.FindAll().FirstOrDefault();
                }
                if (config == null)
                {
                    config = new Config()
                    {
                        Id = ObjectId.NewObjectId().ToString(),
                        RegisterEnabled = true
                    };
                    _configs.Insert(config);
                }
                return config;
            }
            set { }
        }
    }
}
