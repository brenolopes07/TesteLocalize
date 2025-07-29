using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLocalize.Application.DTOs;
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


        public async Task<PagedResult<Company>> ExecuteAsync(Guid userId, int pageNumber, int pageSize)
        {
            if(pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Page number and page size must be greater than zero.");


            var (items, totalCount) = await _companyRepository.GetByUserIdPagedAsync(userId, pageNumber, pageSize);

            return new PagedResult<Company>(items, totalCount, pageNumber, pageSize);

        }
    }
}
