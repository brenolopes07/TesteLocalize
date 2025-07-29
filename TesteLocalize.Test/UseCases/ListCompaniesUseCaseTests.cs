using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TesteLocalize.Application.DTOs;
using TesteLocalize.Application.UseCases;
using TesteLocalize.Domain.Entities;
using TesteLocalize.Domain.Repository;
using Xunit;

namespace TesteLocalize.Tests.UseCases
{
    public class ListCompaniesUseCaseTests
    {
        [Fact]
        public async Task Should_ReturnPagedCompanies_WhenCompaniesExist()
        {
            
            var mockRepo = new Mock<ICompanyRepository>();
            var userId = Guid.NewGuid();
            int pageNumber = 1;
            int pageSize = 2;

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

            int totalCount = 5; // Exemplo total de registros no BD para aquele user

            mockRepo.Setup(r => r.GetByUserIdPagedAsync(userId, pageNumber, pageSize))
                    .ReturnsAsync((companies, totalCount));

            var useCase = new ListCompaniesUseCase(mockRepo.Object);

            
            var result = await useCase.ExecuteAsync(userId, pageNumber, pageSize);

            
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(totalCount);
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.Items.Should().BeEquivalentTo(companies);
        }

        [Fact]
        public async Task Should_ThrowException_WhenPageNumberOrPageSizeIsInvalid()
        {
            
            var mockRepo = new Mock<ICompanyRepository>();
            var useCase = new ListCompaniesUseCase(mockRepo.Object);
            var userId = Guid.NewGuid();

            
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(userId, 0, 10));
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(userId, 1, 0));
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(userId, -1, 5));
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(userId, 1, -5));
        }

        [Fact]
        public async Task Should_ReturnEmptyPagedResult_WhenNoCompaniesFound()
        {
            
            var mockRepo = new Mock<ICompanyRepository>();
            var userId = Guid.NewGuid();
            int pageNumber = 1;
            int pageSize = 10;

            mockRepo.Setup(r => r.GetByUserIdPagedAsync(userId, pageNumber, pageSize))
                    .ReturnsAsync((new List<Company>(), 0));

            var useCase = new ListCompaniesUseCase(mockRepo.Object);

            
            var result = await useCase.ExecuteAsync(userId, pageNumber, pageSize);

            
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
        }
    }
}
