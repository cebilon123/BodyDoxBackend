using System.Threading.Tasks;
using Api.Infrastructure.Errors.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Api.Infrastructure.Repositories
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, string connectionString)
        {
            return services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
        }


        public static IServiceCollection AddRepository<TEntity, TIdentifiable>(this IServiceCollection services,
            string collectionName) where TEntity : class, IIdentifiable<TIdentifiable>
        {
            return services.AddTransient<IMongoGenericRepository<TEntity, TIdentifiable>>(sp =>
            {
                var database = sp.GetService<IMongoClient>()?.GetDatabase("rent_main");

                if (database is null)
                    throw new InfrastructureException("Database_not_found", typeof(IMongoDatabase));

                return new MongoGenericRepository<TEntity, TIdentifiable>(database, collectionName);
            });
        }

        public static IApplicationBuilder UseIndexOnRepository<TEntity, TIdentifiable>(this IApplicationBuilder app,
            IndexKeysDefinition<TEntity> keysDefinition, CreateIndexOptions options)
            where TEntity : class, IIdentifiable<TIdentifiable>
        {
            using var scope = app.ApplicationServices.CreateScope();

            var collection = scope.ServiceProvider.GetService<IMongoGenericRepository<TEntity, TIdentifiable>>()
                ?.Collection;

            Task.Run(async () => await collection?.Indexes.CreateOneAsync(
                new CreateIndexModel<TEntity>(keysDefinition, options))!);

            return app;
        }
    }
}