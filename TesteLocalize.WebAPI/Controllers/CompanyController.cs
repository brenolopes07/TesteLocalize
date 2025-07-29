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
                
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return Unauthorized(new { error = "Invalid user ID in token." });
            }

            var result = await _listCompaniesUseCase.ExecuteAsync(parsedUserId, page, pageSize);

            return Ok(result);
        }
    }
}

