using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TesteLocalize.Application.UseCases;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repository;
using Xunit;

namespace TesteLocalize.Tests.UseCases
{
    public class ListCompaniesUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnCompanies_WhenCompaniesExist()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepository>();
            var userId = Guid.NewGuid();

            var companies = new List<Company>
            {
                new Company(
                     userId,
                    "Company A",
                    "Fantasia A",
                    "12345678000100",
                     "Active",
                     DateTime.Now,
                     "Matriz",
                     "Legal Nature",
                     "Main Activity",
                     "Street A",
                     "100",
                     "",
                    "Neighborhood A",
                    "City A",
                    "ST",
                    "12345000",
                    Guid.NewGuid()
                ),
                new Company(
                    userId,
                    "Company B",
                    "Fantasia B",
                    "98765432000199",
                    "Active",
                    DateTime.Now,
                    "Matriz",
                    "Legal Nature",
                    "Main Activity",
                    "Street B",
                    "200",
                    "",
                    "Neighborhood B",
                    "City B",
                    "ST",
                    "54321000",
                    Guid.NewGuid()
                )
            };

            mockRepo.Setup(r => r.GetByUserIdAsync(userId))
                    .ReturnsAsync(companies);

            var useCase = new ListCompaniesUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(companies);
        }

        [Fact]
        public async Task Should_ThrowException_WhenNoCompaniesFound()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepository>();
            var userId = Guid.NewGuid();

            mockRepo.Setup(r => r.GetByUserIdAsync(userId))
                    .ReturnsAsync((IEnumerable<Company>)null);

            var useCase = new ListCompaniesUseCase(mockRepo.Object);

            // Act
            Func<Task> act = async () => await useCase.ExecuteAsync(userId);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("No companies found for the given user ID.");
        }
    }
}