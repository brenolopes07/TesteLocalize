using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TesteLocalize.Application.DTOs.External;
using TesteLocalize.Application.Interfaces;

namespace TesteLocalize.Infra.Services
{
    public class ReceitaWSService :IReceitaWSService
    {
        private readonly HttpClient _httpClient;    

        public ReceitaWSService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReceitaWSResponseDTO> GetCompanyByCnpjAsync(string cnpj)
        {
            if(string.IsNullOrWhiteSpace(cnpj))            
              throw new ArgumentException("CNPJ cannot be null or empty", nameof(cnpj));

            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching data from ReceitaWS: {response.ReasonPhrase}");
            }

            var dto = await response.Content.ReadFromJsonAsync<ReceitaWSResponseDTO>();

            if (dto == null || dto.Status != "OK")
                throw new Exception($"Invalid response from ReceitaWS: {dto?.Status}");

            return dto;
        }
    }
}
