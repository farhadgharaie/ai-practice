using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CompanyApp.Infrastructure.Mongo;

public class CompanyRepository: ICompanyRepository
{
    private readonly MongoDbContext _context;

    public CompanyRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> SearchByNameAsync(string name)
    {
        var filter = Builders<Company>.Filter.Regex(
            c => c.Name,
            new BsonRegularExpression(name, "i")
        );

        return await _context.Companies.Find(filter).ToListAsync();
    }
    public async Task InsertManyAsync(IEnumerable<Company> companies)
    {
        await _context.Companies.InsertManyAsync(companies);
    }
}