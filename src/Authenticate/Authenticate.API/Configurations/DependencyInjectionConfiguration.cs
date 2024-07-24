using Authenticate.Domain.Configuration;
using Authenticate.Domain.UseCases.Create.Contracts;
using Authenticate.Infra.UseCases.Create;

namespace Authenticate.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ResolveDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IRepository, Repository>();
            builder.Services.AddTransient<IService, Service>();

            builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ApiConfiguration).Assembly));
        }
    }
}
