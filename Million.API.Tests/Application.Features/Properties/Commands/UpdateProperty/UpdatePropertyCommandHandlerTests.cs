using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Features.Properties.Commands.UpdateProperty;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;
using Moq;

namespace Million.Tests.Features.Properties.Commands.UpdateProperty
{
    [TestFixture]
    public class UpdatePropertyCommandHandlerTests
    {
        private Mock<IPropertyRepository> _repoMock;
        private Mock<ILogger<UpdatePropertyCommandHandler>> _loggerMock;
        private UpdatePropertyCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IPropertyRepository>();
            _loggerMock = new Mock<ILogger<UpdatePropertyCommandHandler>>();
            _handler = new UpdatePropertyCommandHandler(_repoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Should_Update_Property_And_Return_Dto_When_Valid()
        {
            // Arrange
            var existingProperty = new Property("Old Name", "Old Address", 100000, "OLD123", 2010, 2);

            var updatedDto = new UpdatePropertyDto
            {
                Name = "New Name",
                Address = "New Address",
                Price = 200000,
                CodeInternal = "NEW123",
                Year = 2022,
                IdOwner = 5
            };

            var command = new UpdatePropertyCommand(1, updatedDto);

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingProperty);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Property>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedDto.Name, result.Name);
            Assert.AreEqual(updatedDto.Address, result.Address);
            Assert.AreEqual(updatedDto.Price, result.Price);
            Assert.AreEqual(updatedDto.CodeInternal, result.CodeInternal);
            Assert.AreEqual(updatedDto.Year, result.Year);
            Assert.AreEqual(updatedDto.IdOwner, result.IdOwner);
        }

        [Test]
        public void Should_Throw_ValidationException_When_Property_Not_Found()
        {
            // Arrange
            var updatedDto = new UpdatePropertyDto
            {
                Name = "New Name",
                Address = "New Address",
                Price = 200000,
                CodeInternal = "NEW123",
                Year = 2022,
                IdOwner = 5
            };
            var command = new UpdatePropertyCommand(1, updatedDto);
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Property)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.IsTrue(ex.Errors.ContainsKey("PropertyId"));
            Assert.AreEqual("Property not found.", ex.Errors["PropertyId"].First());
        }

        [Test]
        public void Should_DatabaseException_When_DbUpdateException_Occurs()
        {
            // Arrange
            var existingProperty = new Property("Old Name", "Old Address", 100000, "OLD123", 2010, 2);
            var updatedDto = new UpdatePropertyDto
            {
                Name = "New Name",
                Address = "New Address",
                Price = 200000,
                CodeInternal = "NEW123",
                Year = 2022,
                IdOwner = 5
            };
            var command = new UpdatePropertyCommand(1, updatedDto);
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingProperty);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Property>())).ThrowsAsync(new Microsoft.EntityFrameworkCore.DbUpdateException());

            // Act & Assert
            var ex = Assert.ThrowsAsync<DatabaseException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.AreEqual("An error occurred while accessing the database.", ex.Message);
        }
    }
}