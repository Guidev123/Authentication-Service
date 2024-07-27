using Authenticate.API.Configurations;
using Authenticate.Domain.UseCases.Authenticate;
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


            // CREATE
            app.MapPost("api/create-user", async (CreateRequest request, IRequestHandler<CreateRequest, CreateResponse> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                if(!result.IsSuccess) return Results.Json(result, statusCode:result.Status);
                return Results.Created($"api/authenticate/{result.Data?.Id}", result);  
            });


            // AUTHENTICATE
            app.MapPost("api/authenticate-user", async (AuthenticateRequest request, IRequestHandler<AuthenticateRequest, AuthenticateResponse> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());

                if (!result.IsSuccess) return Results.Json(result, statusCode: result.Status);
                if (result.Data is null) return Results.Json(result, statusCode: 500);
                result.Data.Token = JwtConfiguration.GenerateJwt(result.Data);
                return Results.Ok(result);
            });
        }
    }
}
