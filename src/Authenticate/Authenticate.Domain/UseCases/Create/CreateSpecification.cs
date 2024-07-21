using Flunt.Notifications;
using Flunt.Validations;

namespace Authenticate.Domain.UseCases.Create
{
    public static class CreateSpecification
    {
        public static Contract<Notification> Assert(CreateRequest request) => new Contract<Notification>().Requires()
            .IsLowerThan(request.Name.Length, 100, "Name", "The name must contain less than 100 characters")
            .IsGreaterThan(request.Name.Length, 3, "Name", "The name must contain more than 3 characters")
            .IsLowerThan(request.Password.Length, 30, "Password", "The password must contain less than 30 characters")
            .IsGreaterThan(request.Password.Length, 8, "Password", "The password must contain more than 8 characters")
            .IsEmail(request.Email, "Email", "Invalid email");

    }
}
