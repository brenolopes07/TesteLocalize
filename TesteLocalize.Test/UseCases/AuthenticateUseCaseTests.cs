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
    public class AuthenticateUserUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockHasher = new Mock<IPasswordHasher>();
            var mockJwt = new Mock<IJwtTokenService>();

            var user = new User("Breno", "breno@email.com", "hashed_pw");

            mockRepo.Setup(r => r.GetByEmailAsync("breno@email.com"))
                    .ReturnsAsync(user);

            mockHasher.Setup(h => h.VerifyPassword("hashed_pw", "123456"))
                      .Returns(true);

            mockJwt.Setup(j => j.GenerateToken(user))
                   .Returns("jwt_token");

            var useCase = new AuthenticateUserUseCase(mockRepo.Object, mockHasher.Object, mockJwt.Object);

            // Act
            var token = await useCase.ExecuteAsync("breno@email.com", "123456");

            // Assert
            token.Should().Be("jwt_token");
        }

        [Fact]
        public async Task Should_ThrowException_WhenUserNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockHasher = new Mock<IPasswordHasher>();
            var mockJwt = new Mock<IJwtTokenService>();

            mockRepo.Setup(r => r.GetByEmailAsync("inexistente@email.com"))
                    .ReturnsAsync((User)null);

            var useCase = new AuthenticateUserUseCase(mockRepo.Object, mockHasher.Object, mockJwt.Object);

            // Act
            Func<Task> act = async () =>
                await useCase.ExecuteAsync("inexistente@email.com", "123");

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Invalid email or password.");
        }

        [Fact]
        public async Task Should_ThrowException_WhenPasswordInvalid()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var mockHasher = new Mock<IPasswordHasher>();
            var mockJwt = new Mock<IJwtTokenService>();

            var user = new User("Breno", "breno@email.com", "hashed_pw");

            mockRepo.Setup(r => r.GetByEmailAsync("breno@email.com"))
                    .ReturnsAsync(user);

            mockHasher.Setup(h => h.VerifyPassword("hashed_pw", "wrong_password"))
                      .Returns(false);

            var useCase = new AuthenticateUserUseCase(mockRepo.Object, mockHasher.Object, mockJwt.Object);

            // Act
            Func<Task> act = async () =>
                await useCase.ExecuteAsync("breno@email.com", "wrong_password");

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Invalid email or password.");
        }
    }
}