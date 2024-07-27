using Authenticate.Domain.Entities;
using Authenticate.Domain.UseCases.Authenticate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using Authenticate.Domain.Configuration;

namespace Authenticate.API.Configurations
{
    public static class JwtConfiguration
    {
        public static string GenerateJwt(AuthenticateResponseData responseData)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR");
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(responseData),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = credentials,
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(AuthenticateResponseData responseData)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("Id", responseData.Id));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, responseData.Name));
            ci.AddClaim(new Claim(ClaimTypes.Name, responseData.Email));

            foreach (var role in responseData.Roles) ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }
    }
}
