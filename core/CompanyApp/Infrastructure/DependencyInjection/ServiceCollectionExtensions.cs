using CompanyApp.Domain.Interfaces;
using CompanyApp.Infrastructure.Mongo;
using CompanyApp.Infrastructure.Mongo.Initialization;
using CompanyApp.Infrastructure.ThirdParty;

namespace CompanyApp.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        // Bind MongoDbSettings
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDb"));

        // Register DbContext
        services.AddSingleton<MongoDbContext>();

        // Repository
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        // Database initializer
        services.AddSingleton<IMongoDbInitializer, MongoDbInitializer>();

        services.AddHttpClient<IThirdPartyCompanySearchService, ThirdPartyCompanySearchService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:1313/");
        });

        return services;
    }
}