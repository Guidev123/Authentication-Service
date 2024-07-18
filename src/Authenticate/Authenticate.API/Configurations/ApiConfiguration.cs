using Authenticate.Domain.Configuration;
using Authenticate.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authenticate.API.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddKeysMiddleware(this WebApplicationBuilder builder)
        {
            SecurityConfiguration.SecurityKeys.ConnectionString =
                builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            SecurityConfiguration.SecurityKeys.ApiKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;

            SecurityConfiguration.SecurityKeys.JwtPrivateKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;

            SecurityConfiguration.SecurityKeys.JwtPrivateKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;
        }
        public static void AddDataBaseMiddleware(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AccountDbContext>(options =>
            options.UseSqlServer(SecurityConfiguration.SecurityKeys.ConnectionString,
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityConfiguration.SecurityKeys.JwtPrivateKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            builder.Services.AddAuthorization();
        }
    }
}
