using Authenticate.Domain.Entities;
using Authenticate.Domain.UseCases.Authenticate.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticate.Infra.UseCases.Authenticate
{
    public class Repository : IRepository
    {
        private readonly AccountDbContext _context;
        public Repository(AccountDbContext context) => _context = context;

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken) =>
            await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
        
    }
}
