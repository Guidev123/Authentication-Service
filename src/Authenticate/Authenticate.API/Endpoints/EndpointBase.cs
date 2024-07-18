using Authenticate.API.Endpoints.Authentication;
using Authenticate.Domain.Entities;

namespace Authenticate.API.Endpoints
{
    public static class EndpointBase
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "It's ok!" });

            endpoints.MapGroup("api/auth")
                .WithTags("Authentication")
                .MapEndpoint<AccountEndpoint>();

        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpointBase
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
