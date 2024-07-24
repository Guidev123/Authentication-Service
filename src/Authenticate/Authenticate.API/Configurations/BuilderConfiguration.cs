using Authenticate.Domain.Configuration;
using Authenticate.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authenticate.API.Configurations
{
    public static class BuilderConfiguration
    {
        public static void AddKeysMiddleware(this WebApplicationBuilder builder)
        {
            ApiConfiguration.SecurityKeys.ConnectionString =
                builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            ApiConfiguration.SecurityKeys.ApiKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;

            ApiConfiguration.SecurityKeys.JwtPrivateKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;

            ApiConfiguration.SecurityKeys.JwtPrivateKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;

            ApiConfiguration.SendGrid.ApiKey =
                builder.Configuration.GetSection("SendGrid").GetValue<string>("ApiKey") ?? string.Empty;

            ApiConfiguration.Email.DefaultFromName =
                builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromName") ?? string.Empty;

            ApiConfiguration.Email.DefaultFromEmail =
                builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromEmail") ?? string.Empty;
        }
        public static void AddDataBaseMiddleware(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AccountDbContext>(options =>
            options.UseSqlServer(ApiConfiguration.SecurityKeys.ConnectionString,
                                    m => m.MigrationsAssembly("Authenticate.Infra")));
        }

        public static void AddJwtAuthenticationMiddleware(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApiConfiguration.SecurityKeys.JwtPrivateKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            builder.Services.AddAuthorization();
        }


        public static void AddEnvMiddleware(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }


        public static void AddDocumentationConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger();/*RequireAuthorization();*/
        }
    }
}
