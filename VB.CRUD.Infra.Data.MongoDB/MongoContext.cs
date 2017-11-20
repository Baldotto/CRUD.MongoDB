using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using VB.CRUD.Domain;

namespace VB.CRUD.Infra.Data.MongoDB
{
    public class MongoContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MongoContext(string connectionString, string database)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(database);
            Map();
        }

        public IMongoCollection<Post> Posts
        {
            get
            {
                return _database.GetCollection<Post>("Posts");
            }
        }

        private void Map()
        {            
            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
            });
        }
    }
}
