
using TesteLocalize.Application.Interfaces;
using TesteLocalize.Domain.Repositories;

namespace TesteLocalize.Application.UseCases
{
    public class AuthenticateUserUseCase
    {
        private readonly IUserRepository _userepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticateUserUseCase(IUserRepository userepository, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
        {
            _userepository = userepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> ExecuteAsync(string email, string password)
        {
            var user = await _userepository.GetByEmailAsync(email);

            if (user == null)
                throw new Exception("Invalid email or password.");

            if(!_passwordHasher.VerifyPassword(user.PasswordHash, password))
            {
                throw new Exception("Invlaid email or password.");
            }

            return _jwtTokenService.GenerateToken(user);
        }
    }
}
