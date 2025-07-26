
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
        
        public async Task AddAsync (Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Company>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Companies.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<bool> ExistsByCnpjAsync(string cnpj, Guid userId)
        {
            return await _context.Companies.AnyAsync(c => c.CNPJ == cnpj && c.UserId == userId);
        }

       
    }
}
