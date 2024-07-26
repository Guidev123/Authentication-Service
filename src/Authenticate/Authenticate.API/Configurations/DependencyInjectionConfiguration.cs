using Authenticate.Domain.Configuration;
using Authenticate.Domain.UseCases.Authenticate.Contracts;
using Authenticate.Domain.UseCases.Create.Contracts;
using Authenticate.Infra.UseCases.Authenticate;
using Authenticate.Infra.UseCases.Create;

namespace Authenticate.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ResolveDependencies(this WebApplicationBuilder builder)
        {
            // CREATE DEPENDENCIES
            builder.Services.AddTransient<
                Authenticate.Domain.UseCases.Create.Contracts.IRepository,
                Authenticate.Infra.UseCases.Create.Repository>();
            builder.Services.AddTransient<IService, Service>();



            // AUTH DEPENDENCIES
            builder.Services.AddTransient<
                Authenticate.Domain.UseCases.Authenticate.Contracts.IRepository,
                Authenticate.Infra.UseCases.Authenticate.Repository>();


            builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ApiConfiguration).Assembly));
        }
    }
}
