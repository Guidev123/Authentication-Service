using Authenticate.Domain.UseCases.Create;
using MediatR;

namespace Authenticate.API.Endpoints
{
    public static class AccountEndpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "Está funcinando!" });

            app.MapPost("api/authenticate", async (CreateRequest request, IRequestHandler<CreateRequest, CreateResponse> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                if(!result.IsSuccess) return Results.Json(result, statusCode:result.Status);
                return Results.Created("", result);  
            });
        }
    }
}
