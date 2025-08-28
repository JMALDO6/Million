using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Features.Properties.Commands.ChangePropertyPrice;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;
using Moq;

namespace Million.Tests.Features.Properties.Commands.ChangePropertyPrice
{
    [TestFixture]
    public class ChangePropertyPriceCommandHandlerTests
    {
        private Mock<IPropertyRepository> _repoMock;
        private Mock<ILogger<ChangePropertyPriceCommandHandler>> _loggerMock;
        private ChangePropertyPriceCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IPropertyRepository>();
            _loggerMock = new Mock<ILogger<ChangePropertyPriceCommandHandler>>();
            _handler = new ChangePropertyPriceCommandHandler(_repoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Should_Update_Price_When_Property_Exists()
        {
            // Arrange
            var property = new Property("Test Property", "address", 150000, "CODE", 1990, 1);
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(property);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Property>())).Returns(Task.CompletedTask);

            // Act
            var command = new ChangePropertyPriceCommand(1, 150000);
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repoMock.Verify(r => r.UpdateAsync(It.Is<Property>(p => p.Price == 150000)), Times.Once);
        }

        [Test]
        public void Should_Throw_When_NewPrice_Invalid()
        {
            // Arrange
            var command = new ChangePropertyPriceCommand(1, -50000);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Errors.Any(e => e.Key == "NewPrice"));
        }

        [Test]
        public void Should_Throw_When_Property_Not_Found()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Property)null);
            var command = new ChangePropertyPriceCommand(99, 150000);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Should_Throw_DatabaseException_On_DbUpdateError()
        {
            // Arrange
            var property = new Property("Test Property", "address", 150000, "CODE", 1990, 1);
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(property);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Property>())).ThrowsAsync(new Microsoft.EntityFrameworkCore.DbUpdateException());
            var command = new ChangePropertyPriceCommand(1, 150000);

            // Act & Assert
            var ex = Assert.ThrowsAsync<DatabaseException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
