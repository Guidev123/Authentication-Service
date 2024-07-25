using Authenticate.API.Configurations;
using Authenticate.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddEnvMiddleware();
builder.AddKeysMiddleware();
builder.AddDataBaseMiddleware();
builder.AddJwtAuthenticationMiddleware();
builder.ResolveDependencies();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.Run();
