using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Features.Properties.Commands.CreateProperty;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;
using Moq;

namespace Million.Tests.Features.Properties.Commands.CreateProperty
{
    [TestFixture]
    public class CreatePropertyCommandHandlerTests
    {
        private Mock<IPropertyRepository> _repoMock;
        private Mock<ILogger<CreatePropertyCommandHandler>> _loggerMock;
        private CreatePropertyCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IPropertyRepository>();
            _loggerMock = new Mock<ILogger<CreatePropertyCommandHandler>>();
            _handler = new CreatePropertyCommandHandler(_repoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Should_Create_Property_And_Return_Dto_When_Valid()
        {
            // Arrange
            var dto = new CreatePropertyDto
            {
                Name = "Casa Bonita",
                Address = "Calle 123",
                Price = 300000,
                CodeInternal = "CB123",
                Year = 2020,
                IdOwner = 5
            };

            var command = new CreatePropertyCommand(dto);

            _repoMock.Setup(r => r.ExistsAsync(It.IsAny<PropertyFilterDto>())).ReturnsAsync(false);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Property>()))
                     .Callback<Property>(p => p.IdProperty = 99)
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(99, result.PropertyId);
            Assert.AreEqual(dto.Name, result.Name);
            Assert.AreEqual(dto.CodeInternal, result.CodeInternal);
        }

        [Test]
        public void Should_Throw_ValidationException_When_CodeInternal_Exists()
        {
            // Arrange
            var dto = new CreatePropertyDto
            {
                Name = "Casa Bonita",
                Address = "Calle 123",
                Price = 300000,
                CodeInternal = "CB123",
                Year = 2020,
                IdOwner = 5
            };
            var command = new CreatePropertyCommand(dto);
            _repoMock.Setup(r => r.ExistsAsync(It.IsAny<PropertyFilterDto>())).ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.IsTrue(ex.Errors.ContainsKey("CodeInternal"));
        }

        [Test]
        public void Should_Throw_DatabaseException_On_DbUpdateException()
        {
            // Arrange
            var dto = new CreatePropertyDto
            {
                Name = "Casa Bonita",
                Address = "Calle 123",
                Price = 300000,
                CodeInternal = "CB123",
                Year = 2020,
                IdOwner = 5
            };
            var command = new CreatePropertyCommand(dto);
            _repoMock.Setup(r => r.ExistsAsync(It.IsAny<PropertyFilterDto>())).ReturnsAsync(false);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Property>())).ThrowsAsync(new Microsoft.EntityFrameworkCore.DbUpdateException());

            // Act & Assert
            Assert.ThrowsAsync<DatabaseException>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}