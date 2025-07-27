using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLocalize.Application.Interfaces;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repositories;

namespace TesteLocalize.Application.UseCases
{
    public class RegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;


        public RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> ExecuteAsync(string name, string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if(existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var passwordHash = _passwordHasher.HashPassword(password);

            var user = new User(name, email, passwordHash);

            await _userRepository.AddAsync(user);

            return user;
        }
    }
}
