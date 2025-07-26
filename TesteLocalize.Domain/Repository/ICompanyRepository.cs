

using TesteLocalize.Domain.Entities;

namespace TesteLocalize.Domain.Repository
{
    public interface ICompanyRepository
    {

        Task AddAsync(Company Company);
        Task<IEnumerable<Company>> GetByUserIdAsync(Guid userId);
        Task<bool> ExistsByCnpjAsync(string cnpj, Guid userId);

    }
}
