using JohaRepository.Interfaces;

using MongoDB.Bson.Serialization.Attributes;
using System;

namespace JohaMongoLogger
{
    public class LoggingModel<TModel>:IDomain<string>
        where TModel : class
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public TModel Model { get; set; }
        public DateTime DateTime { get; set; }
    }
}
