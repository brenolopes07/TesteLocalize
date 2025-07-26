using TesteLocalize.Domain.Entities;

namespace TesteLocalize.Domain.Repositories
{
    public interface IUserRepository
    {

        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);

        Task AddAsync(User user);
    }
}
