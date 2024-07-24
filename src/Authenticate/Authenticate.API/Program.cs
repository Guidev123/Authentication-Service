using Authenticate.API.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.AddEnvMiddleware();
builder.AddKeysMiddleware();
builder.AddDataBaseMiddleware();
builder.AddJwtAuthenticationMiddleware();
builder.ResolveDependencies();
builder.AddDocumentationConfig();

var app = builder.Build();

app.ConfigureDevEnvironment();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
