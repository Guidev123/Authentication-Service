using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Domain.UseCases.Create.Contracts
{
    public interface IRepository
    {
        Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
    }
}
