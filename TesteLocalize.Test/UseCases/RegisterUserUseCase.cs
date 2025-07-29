using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using TesteLocalize.Application.UseCases;
using TesteLocalize.Application.Interfaces;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repositories;

namespace TesteLocalize.Tests.UseCases
{
    public class RegisterUserUseCaseTests
    {
        [Fact]
        public async Task Should_RegisterUser_WhenEmailNotExists()
        {
            
            var mockUserRepo = new Mock<IUserRepository>();
            var mockPasswordHasher = new Mock<IPasswordHasher>();

            mockUserRepo.Setup(r => r.GetByEmailAsync("breno@email.com"))
                        .ReturnsAsync((User)null); // usuário ainda não existe

            mockPasswordHasher.Setup(h => h.HashPassword("123456"))
                              .Returns("hashed_password");

            var useCase = new RegisterUserUseCase(mockUserRepo.Object, mockPasswordHasher.Object);

            
            var result = await useCase.ExecuteAsync("Breno", "breno@email.com", "123456");

            
            result.Should().NotBeNull();
            result.Name.Should().Be("Breno");
            result.Email.Should().Be("breno@email.com");
            result.PasswordHash.Should().Be("hashed_password");

            mockUserRepo.Verify(r => r.AddAsync(It.Is<User>(u =>
                u.Name == "Breno" &&
                u.Email == "breno@email.com" &&
                u.PasswordHash == "hashed_password"
            )), Times.Once);
        }

        [Fact]
        public async Task Should_ThrowException_WhenEmailAlreadyExists()
        {
            
            var existingUser = new User("Breno", "breno@email.com", "existing_hash");

            var mockUserRepo = new Mock<IUserRepository>();
            var mockPasswordHasher = new Mock<IPasswordHasher>();

            mockUserRepo.Setup(r => r.GetByEmailAsync("breno@email.com"))
                        .ReturnsAsync(existingUser);

            var useCase = new RegisterUserUseCase(mockUserRepo.Object, mockPasswordHasher.Object);

            
            Func<Task> act = async () =>
                await useCase.ExecuteAsync("Novo Nome", "breno@email.com", "nova_senha");

           
            await act.Should()
                     .ThrowAsync<InvalidOperationException>()
                     .WithMessage("User with this email already exists.");

            mockUserRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
