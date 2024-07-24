using MediatR;

namespace Authenticate.Domain.UseCases.Create
{
    public record CreateRequest(string Name, string Email, string Password) : IRequest<CreateResponse>;
}
