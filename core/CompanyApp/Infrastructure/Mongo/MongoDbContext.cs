using CompanyApp.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace CompanyApp.Infrastructure.Mongo;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDbSettings _settings;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;

        RegisterMappings();

        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.Database);
    }

    public IMongoCollection<Company> Companies =>
        _database.GetCollection<Company>(_settings.CompaniesCollection);
    public IMongoDatabase Database => _database;
    private static void RegisterMappings()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(Company)))
        {
            BsonClassMap.RegisterClassMap<Company>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.MapMember(c => c.Name).SetElementName("name");
            });
        }
    }
}