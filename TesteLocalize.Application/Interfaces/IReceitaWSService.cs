

using TesteLocalize.Application.DTOs.External;

namespace TesteLocalize.Application.Interfaces
{
    public interface IReceitaWSService
    {
        Task<ReceitaWSResponseDTO> GetCompanyByCnpjAsync(string cnpj);
    }
}
