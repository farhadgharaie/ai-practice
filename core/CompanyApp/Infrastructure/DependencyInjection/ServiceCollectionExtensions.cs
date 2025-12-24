using CompanyApp.Domain.Interfaces;
using CompanyApp.Infrastructure.Mongo;
using CompanyApp.Infrastructure.Mongo.Initialization;

namespace CompanyApp.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // services.Configure<MongoDbSettings>(
        //     configuration.GetSection("MongoDb"));
        //
        // services.AddSingleton<MongoDbContext>();
        // services.AddScoped<ICompanyRepository, CompanyRepository>();
        
        // Bind MongoDbSettings
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDb"));

        // Register DbContext
        services.AddSingleton<MongoDbContext>();

        // Repository
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        // Database initializer
        services.AddSingleton<IMongoDbInitializer, MongoDbInitializer>();


        return services;
    }
}