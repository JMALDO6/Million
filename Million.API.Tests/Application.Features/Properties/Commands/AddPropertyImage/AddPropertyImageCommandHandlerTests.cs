using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Features.Properties.Commands.AddPropertyImage;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;
using Moq;

namespace Million.Tests.Features.Properties.Commands.AddPropertyImage
{
    [TestFixture]
    public class AddPropertyImageCommandHandlerTests
    {
        private Mock<IPropertyImageRepository> _repoMock;
        private Mock<ILogger<AddPropertyImageCommandHandler>> _loggerMock;
        private AddPropertyImageCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IPropertyImageRepository>();
            _loggerMock = new Mock<ILogger<AddPropertyImageCommandHandler>>();
            _handler = new AddPropertyImageCommandHandler(_repoMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Should_Save_Image_And_Return_Dto_When_Valid()
        {
            // Arrange
            var imageBytes = new byte[] { 1, 2, 3 };
            var stream = new MemoryStream(imageBytes);
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(imageBytes.Length);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                    .Returns((Stream s, CancellationToken _) => stream.CopyToAsync(s));

            var command = new AddPropertyImageCommand(1, new AddPropertyImageDto { ImageFile = fileMock.Object });

            var savedEntity = new PropertyImage
            {
                IdPropertyImage = 10,
                IdProperty = 1,
                Enabled = true,
                File = imageBytes
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<PropertyImage>())).ReturnsAsync(savedEntity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.IdPropertyImage);
            Assert.AreEqual(1, result.IdProperty);
            Assert.IsTrue(result.Enabled);
        }

        [Test]
        public void Should_Throw_ValidationException_When_ImageFile_Is_Null()
        {
            // Arrange
            var command = new AddPropertyImageCommand(1, new AddPropertyImageDto { ImageFile = null });

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.IsTrue(ex.Errors.ContainsKey("ImageFile"));
        }

        [Test]
        public void Should_Throw_When_Repository_Throws_DbUpdateException()
        {
            // Arrange
            var imageBytes = new byte[] { 1, 2, 3 };
            var stream = new MemoryStream(imageBytes);
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(imageBytes.Length);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                    .Returns((Stream s, CancellationToken _) => stream.CopyToAsync(s));
            var command = new AddPropertyImageCommand(1, new AddPropertyImageDto { ImageFile = fileMock.Object });
            _repoMock.Setup(r => r.AddAsync(It.IsAny<PropertyImage>())).ThrowsAsync(new Microsoft.EntityFrameworkCore.DbUpdateException());

            // Act & Assert
            Assert.ThrowsAsync<DatabaseException>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}