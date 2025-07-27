using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repository;

namespace TesteLocalize.Application.UseCases
{
    public class ListCompaniesUseCase
    {
        private readonly ICompanyRepository _companyRepository;

        public ListCompaniesUseCase(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }


        public async Task<IEnumerable<Company>> ExecuteAsync(Guid userId)
        {
            var companies =  await _companyRepository.GetByUserIdAsync(userId);
            if(companies == null)
                throw new Exception("No companies found for the given user ID.");

            return companies;
        }
    }
}
