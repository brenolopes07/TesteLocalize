using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using TesteLocalize.Application.DTOs.External;
using TesteLocalize.Application.Interfaces;
using TesteLocalize.Application.UseCases;
using TesteLocalize.Application.DTOs;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repository;
using Xunit;
using static TesteLocalize.Application.DTOs.External.ReceitaWSResponseDTO;

namespace TesteLocalize.Tests.UseCases
{
    public class RegisterCompanyUseCaseTests
    {
        [Fact]
        public async Task Should_ThrowException_WhenCompanyAlreadyExists()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepository>();
            var mockReceita = new Mock<IReceitaWSService>();
            var userId = Guid.NewGuid();
            var cnpj = "12345678901234";

            mockRepo.Setup(r => r.ExistsByCnpjAsync(cnpj, userId))
                    .ReturnsAsync(true);

            var useCase = new RegisterCompanyUseCase(mockRepo.Object, mockReceita.Object);

            // Act
            var act = async () => await useCase.ExecuteAsync(userId, cnpj);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Company with this CNPJ already exists for the user.");
        }

        [Fact]
        public async Task Should_RegisterCompany_WhenCompanyDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepository>();
            var mockReceita = new Mock<IReceitaWSService>();
            var userId = Guid.NewGuid();
            var cnpj = "12345678901234";

            mockRepo.Setup(r => r.ExistsByCnpjAsync(cnpj, userId))
                    .ReturnsAsync(false);

            var receitaDto = new ReceitaWSResponseDTO
            {
                Name = "Empresa Teste",
                FantasyName = "Fantasia Teste",
                CNPJ = cnpj,
                Situation = "Ativa",
                Opening = "2020-01-01",
                Type = "MATRIZ",
                LegalNature = "Sociedade Limitada",
                MainActivities = new List<ActivityDto> {
                    new ActivityDto { Text = "Desenvolvimento de software" }
                },
                Street = "Rua Teste",
                Number = "123",
                Complement = "Sala 1",
                Neighborhood = "Centro",
                City = "São Paulo",
                State = "SP",
                ZipCode = "01000-000"
            };

            mockReceita.Setup(r => r.GetCompanyByCnpjAsync(cnpj))
                       .ReturnsAsync(receitaDto);

            var useCase = new RegisterCompanyUseCase(mockRepo.Object, mockReceita.Object);

            // Act
            var result = await useCase.ExecuteAsync(userId, cnpj);

            // Assert
            result.Should().NotBeNull();
            result.CNPJ.Should().Be(cnpj);
            result.Name.Should().Be("Empresa Teste");
            result.MainActivity.Should().Be("Desenvolvimento de software");
            result.UserId.Should().Be(userId);
        }
    }
}
