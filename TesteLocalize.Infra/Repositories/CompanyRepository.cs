
using Microsoft.EntityFrameworkCore;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repository;
using TesteLocalize.Infra.Data;

namespace TesteLocalize.Infra.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TesteLocalizeDbContext _context;

        public CompanyRepository(TesteLocalizeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByCnpjAsync(string cnpj, Guid userId)
        {
            return await _context.Companies.AnyAsync(c => c.CNPJ == cnpj && c.UserId == userId);
        }

        public async Task<(IEnumerable<Company> Items, int TotalCount)> GetByUserIdPagedAsync(Guid userId, int pageNumber, int pageSize)
        {
            var query = _context.Companies.Where(c => c.UserId == userId);
            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.OpeningDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
