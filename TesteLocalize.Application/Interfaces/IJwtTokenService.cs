
using TesteLocalize.Domain.Entities;

namespace TesteLocalize.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
