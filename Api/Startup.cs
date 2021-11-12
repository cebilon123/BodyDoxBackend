using System;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Helpers.Swagger;
using Api.Infrastructure.Auth;
using Api.Infrastructure.Auth.Models;
using Api.Infrastructure.Errors;
using Api.Infrastructure.Initialize;
using Api.Infrastructure.Repositories;
using Api.Infrastructure.Repositories.Documents;
using Api.Infrastructure.Repositories.Files;
using JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            BaseConfiguration(services);
            services.AddJwyKeysProvider(new DevelopmentJwtKeyProvider(Configuration["AccessTokenKey"],
                Configuration["RefreshTokenKey"]));
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            BaseConfiguration(services);
            services.AddJwyKeysProvider(new ReleaseDevelopmentKeysProvider());
        }

        // base configuration for both environments. 
        private async void BaseConfiguration(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFile("Logs/app_{0:yyyy}-{0:MM}-{0:dd}.log",
                    fileLoggerOpts =>
                    {
                        fileLoggerOpts.FormatLogFileName = fName => string.Format(fName, DateTime.UtcNow);
                    });
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Api", Version = "v1"});
                c.OperationFilter<ApplySwaggerDescriptionFilter>();
            });
            services
                .AddHttpContextAccessor()
                .AddCommandDispatcher()
                .AddQueryDispatcher()
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddEventBroker()
                .AddEventHandlers()
                .AddExceptionToErrorMapper<ExceptionToResponseMapper>()
                .AddMongoDb(Configuration["DatabaseConnectionString"])
                .AddPasswordService<PasswordService>()
                .AddJwtService<JwtService>()
                .AddRepository<UserDocument, Guid>("users")
                .AddRepository<UserSessionDocument, Guid>("userSessions")
                .AddRepository<OfferDocument, Guid>("offers");

            services.AddRequestInfoProvider<RequestInfoProvider>();

            services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IUserSessionsRepository, UserSessionRepository>()
                .AddTransient<IOfferRepository, OfferRepository>()
                .AddTransient<IImageRepository, ImageRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JwtCookiesMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseIndexOnRepository<UserSessionDocument, Guid>(
                Builders<UserSessionDocument>.IndexKeys.Ascending(u => u.CreatedAt), new CreateIndexOptions
                {
                    ExpireAfter = TimeSpan.FromMinutes(double.Parse(Configuration["sessionTimeMinutes"]))
                });
            app.UseIndexOnRepository<OfferDocument, Guid>(
                Builders<OfferDocument>.IndexKeys.Geo2DSphere(o => o.Location), new CreateIndexOptions
                {
                    Background = true
                });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}