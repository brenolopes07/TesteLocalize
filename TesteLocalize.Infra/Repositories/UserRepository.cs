
using Microsoft.EntityFrameworkCore;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repositories;
using TesteLocalize.Infra.Data;


namespace TesteLocalize.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TesteLocalizeDbContext _context;

        public UserRepository(TesteLocalizeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync (User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);                
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
