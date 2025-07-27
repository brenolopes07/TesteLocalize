

namespace TesteLocalize.Application.Interfaces
{
    public interface IPasswordHasher
    {

        string HashPassword(string password);
        bool VerifyPassword(string hash, string password);
    }
}
