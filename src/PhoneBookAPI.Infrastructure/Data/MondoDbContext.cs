using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PhoneBookAPI.Core.Entities;
using PhoneBookAPI.Infrastructure.Data.Configurations;

namespace PhoneBookAPI.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbSettings _settings;

        public MongoDbContext(MongoDbSettings settings)
        {
            _settings = settings;
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
            
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<Person> Persons => 
            _database.GetCollection<Person>(_settings.PersonCollectionName);
    }
}