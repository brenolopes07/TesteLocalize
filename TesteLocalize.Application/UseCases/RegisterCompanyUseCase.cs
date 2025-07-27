using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLocalize.Application.Interfaces;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repository;

namespace TesteLocalize.Application.UseCases
{
    public class RegisterCompanyUseCase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IReceitaWSService _receitaWsService;

        public RegisterCompanyUseCase(ICompanyRepository companyRepository, IReceitaWSService receitaWSService)
        {
            _companyRepository = companyRepository;
            _receitaWsService = receitaWSService;
        }


        public async Task<Company> ExecuteAsync(Guid userId, string cnpj)
        {
            if (await _companyRepository.ExistsByCnpjAsync(cnpj, userId))
            {
                throw new InvalidOperationException("Company with this CNPJ already exists for the user.");
            }

            var receitaDto = await _receitaWsService.GetCompanyByCnpjAsync(cnpj);

            DateTime? openingDate = null;
            if (DateTime.TryParse(receitaDto.Opening, out var parsedDate))
            {
                openingDate = parsedDate;
            }

            var mainActivity = receitaDto.MainActivities?.Count > 0 ? receitaDto.MainActivities[0].Text : null;

            var company = new Company
                (
                id: Guid.NewGuid(),
                userId: userId,
                name: receitaDto.Name,
                fantasyName: receitaDto.FantasyName,
                cnpj: receitaDto.CNPJ,
                situation: receitaDto.Situation,
                openingDate: openingDate ?? default(DateTime),
                type: receitaDto.Type,
                legalNature: receitaDto.LegalNature,
                mainActivity: mainActivity,
                street: receitaDto.Street,
                number: receitaDto.Number,
                complement: receitaDto.Complement,
                neighborhood: receitaDto.Neighborhood,
                city: receitaDto.City,
                state: receitaDto.State,
                zipCode: receitaDto.ZipCode
                );

            return company;
        }
    }
 }
