using Authenticate.Domain.UseCases.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.UseCases.Authenticate
{
    public record AuthenticateRequest(string Email, string Password) : IRequest<AuthenticateResponse>;
}
