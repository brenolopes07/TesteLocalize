using System.ComponentModel.DataAnnotations;

namespace TesteLocalize.WebAPI.Models
{
    public class CompanyRegisterRequest
    {
        [Required]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "CNPJ must be 14 digits.")]
        public string Cnpj { get; set; }
    }
}
