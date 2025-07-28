using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TesteLocalize.Application.UseCases;
using TesteLocalize.WebAPI.Models;

namespace TesteLocalize.WebAPI.Controllers
{
    [ApiController]
    [Route("api/companies")]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly RegisterCompanyUseCase _registerCompanyUseCase;
        private readonly ListCompaniesUseCase _listCompaniesUseCase;

        public CompanyController(RegisterCompanyUseCase registerCompanyUseCase, ListCompaniesUseCase listCompaniesUseCase)
        {
            _registerCompanyUseCase = registerCompanyUseCase;
            _listCompaniesUseCase = listCompaniesUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CompanyRegisterRequest request)
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
                if (!Guid.TryParse(userIdString, out var userId))
                    return Unauthorized(new { error = "Invalid user ID in token." });

                var company = await _registerCompanyUseCase.ExecuteAsync(userId, request.Cnpj);

                var response = new CompanyResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    FantasyName = company.FantasyName,
                    Cnpj = company.CNPJ,
                    Situation = company.Situation,
                    OpeningDate = company.OpeningDate,
                    Type = company.Type,
                    LegalNature = company.LegalNature,
                    MainActivity = company.MainActivity,
                    Street = company.Street,
                    Number = company.Number,
                    Complement = company.Complement,
                    Neighborhood = company.Neighborhood,
                    City = company.City,
                    State = company.State,
                    ZipCode = company.ZipCode
                };

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
                if (!Guid.TryParse(userIdString, out var userId))
                    return Unauthorized(new { error = "Invalid user ID in token." });

                var companies = await _listCompaniesUseCase.ExecuteAsync(userId);

                var response = companies.Select(c => new CompanyResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    FantasyName = c.FantasyName,
                    Cnpj = c.CNPJ,
                    Situation = c.Situation,
                    OpeningDate = c.OpeningDate,
                    Type = c.Type,
                    LegalNature = c.LegalNature,
                    MainActivity = c.MainActivity,
                    Street = c.Street,
                    Number = c.Number,
                    Complement = c.Complement,
                    Neighborhood = c.Neighborhood,
                    City = c.City,
                    State = c.State,
                    ZipCode = c.ZipCode
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

