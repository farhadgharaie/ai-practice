using MongoDB.Driver;
using CompanyApp.Domain.Entities;

namespace CompanyApp.Infrastructure.Mongo.Initialization;

public interface IMongoDbInitializer
{
    Task InitializeAsync();
}

public class MongoDbInitializer : IMongoDbInitializer
{
    private readonly MongoDbContext _context;

    public MongoDbInitializer(MongoDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await CreateCollectionsIfNotExist();
        await SeedInitialCompanies();
    }

    private async Task CreateCollectionsIfNotExist()
    {
        var existing = await _context.Database.ListCollectionNames().ToListAsync();

        if (!existing.Contains("companies"))
        {
            await _context.Database.CreateCollectionAsync("companies");
        }
    }

    private async Task SeedInitialCompanies()
    {
        var collection = _context.Companies;

        long count = await collection.CountDocumentsAsync(_ => true);
        if (count > 0) return;

        var predefined = new[]
        {
            new Company { Id = Guid.NewGuid().ToString(), Name = "ConSmart" },
            new Company { Id = Guid.NewGuid().ToString(), Name = "TechNova" },
            new Company { Id = Guid.NewGuid().ToString(), Name = "Apex Solutions" }
        };

        await collection.InsertManyAsync(predefined);
    }
}