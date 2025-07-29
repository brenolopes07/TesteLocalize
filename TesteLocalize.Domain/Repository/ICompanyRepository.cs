

using TesteLocalize.Domain.Entities;

namespace TesteLocalize.Domain.Repository
{
    public interface ICompanyRepository
    {

        Task AddAsync(Company Company);
        Task <(IEnumerable<Company>Items, int TotalCount)> GetByUserIdPagedAsync(Guid userId, int pageNumber, int pageSize);
        Task<bool> ExistsByCnpjAsync(string cnpj, Guid userId);

    }
}
