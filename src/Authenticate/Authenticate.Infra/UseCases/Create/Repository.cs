using Authenticate.Domain.Entities;
using Authenticate.Domain.UseCases.Create.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.Infra.UseCases.Create
{
    public class Repository : IRepository
    {
        private readonly AccountDbContext _context;

        public Repository(AccountDbContext context) => _context = context;

        public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken) =>
               await _context.Users.AsNoTracking().AnyAsync(x => x.Email == email);

        public async Task SaveAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
