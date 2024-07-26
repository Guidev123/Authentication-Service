using Authenticate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.UseCases.Authenticate.Contracts
{
    public interface IRepository
    {
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}
