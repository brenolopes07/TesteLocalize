using System.ComponentModel.DataAnnotations;

namespace TesteLocalize.WebAPI.Models
{
    public class UserRegisterRequest
    {
        [Required]
        [StringLength(150, MinimumLength =2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }    
    }
}
