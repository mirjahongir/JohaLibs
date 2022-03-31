using JohaRepository.Interfaces;
using JohaRepository.Models;

using MongoDB.Bson;
using MongoDB.Driver;

using System;

namespace JohaMongoLogger
{
    public class JohaLogger<T> : IJohaLogger<T>
        where T : class
    {
        IMongoCollection<LoggingModel<LoggerModel>> _log;
        public JohaLogger(string connectionString, string dataBase, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var data = client.GetDatabase(dataBase);
            _log = data.GetCollection<LoggingModel<LoggerModel>>(collectionName);
        }

        public void AddLogger(LoggerModel m)
        {
            try
            {
                var model = new JohaMongoLogger.LoggingModel<LoggerModel>()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    DateTime = DateTime.Now,
                    Model = m
                };
                _log.InsertOne(model);
            }
            catch (Exception ext)
            {
                Console.WriteLine(ext.Message);

            }


        }

    }
}
