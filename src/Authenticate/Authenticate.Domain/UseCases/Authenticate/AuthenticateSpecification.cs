using Flunt.Notifications;
using Flunt.Validations;

namespace Authenticate.Domain.UseCases.Authenticate
{
    public static class AuthenticateSpecification
    {
        public static Contract<Notification> Assert(AuthenticateRequest request) => new Contract<Notification>().Requires()
            .IsLowerThan(request.Password.Length, 30, "Password", "The password must contain less than 30 characters")
            .IsGreaterThan(request.Password.Length, 8, "Password", "The password must contain more than 8 characters")
            .IsEmail(request.Email, "Email", "Invalid email");
    }
}
