using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Million.API.Controllers;
using Million.Application.Features.Properties.Commands.ChangePropertyPrice;
using Million.Application.Features.Properties.Commands.CreateProperty;
using Million.Application.Features.Properties.Commands.UpdateProperty;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Features.Properties.Queries.GetProperties;
using Moq;
using NUnit.Framework.Internal;

namespace Million.API.Tests.Controllers
{
    [TestFixture]
    public class PropertyControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private Mock<ILogger<PropertiesController>> _logger;
        private PropertiesController controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<PropertiesController>>();

            controller = new PropertiesController(_mockMediator.Object, _logger.Object);
        }

        [Test]
        public void WhenCreateProperty_ShouldReturnCreatedProperty_IfSuccessful()
        {
            // Arrange
            var createPropertyDto = new CreatePropertyDto
            {
                Name = "Beautiful Home",
                Address = "123 Main St",
                Price = 250000,
                CodeInternal = "BH123",
            };
            var expectedPropertyDto = new PropertyDto
            {
                PropertyId = 1,
                Address = "123 Main St",
                Price = 250000,
                Name = "Beautiful Home",
                CodeInternal = "BH123"
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<CreatePropertyCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedPropertyDto);
            // Act
            var result = controller.Create(createPropertyDto).Result as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(expectedPropertyDto, result.Value);
        }

        [Test]
        public void WhenUpdateProperty_ShouldReturnUpdatedProperty_IfSuccessful()
        {
            // Arrange
            var createPropertyDto = new CreatePropertyDto
            {
                Name = "Beautiful Home",
                Address = "123 Main St",
                Price = 250000,
                CodeInternal = "BH123",
            };
            var expectedPropertyDto = new PropertyDto
            {
                PropertyId = 1,
                Address = "123 Main St",
                Price = 250000,
                Name = "Beautiful Home",
                CodeInternal = "BH123"
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<CreatePropertyCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedPropertyDto);

            // Act
            var result = controller.Create(createPropertyDto).Result as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(expectedPropertyDto, result.Value);
        }

        [Test]
        public void WhenGetProperties_ShouldReturnListOfProperties_IfSuccessful()
        {
            // Arrange
            var expectedProperties = new List<PropertyListItemDto>
            {
                new PropertyListItemDto
                {
                    IdProperty = 1,
                    Name = "Beautiful Home",
                    Address = "123 Main",
                    CodeInternal = "BH123",
                    Price = 250000,
                    Year = 2020,
                    OwnerName = "John Doe"
                }
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetPropertiesQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedProperties);
            var filters = new PropertyFilterDto();

            // Act
            var result = controller.GetProperties(filters).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedProperties, result.Value);
        }

        [Test]
        public void WhenChangePrice_ShouldReturnNoContent_IfSuccessful()
        {
            // Arrange
            var propertyId = 1;
            var changePropertyPriceDto = new ChangePropertyPriceDto
            {
                NewPrice = 300000
            };

            // Act
            var result = controller.ChangePrice(propertyId, changePropertyPriceDto).Result as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void WhenUpdateProperty_ShouldReturnNoContent_IfSuccessful()
        {
            // Arrange
            var propertyId = 1;
            var updatePropertyDto = new UpdatePropertyDto
            {
                Name = "Updated Home",
                Address = "123 Main St",
                Price = 300000,
                CodeInternal = "UH123",
            };
            var expectedPropertyDto = new UpdatePropertyDto
            {
                Address = "123 Main St",
                Price = 250000,
                Name = "Beautiful Home",
                CodeInternal = "BH123"
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdatePropertyCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedPropertyDto);

            // Act
            var result = controller.UpdateProperty(propertyId, updatePropertyDto).Result as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(expectedPropertyDto, result.Value);
        }
    }
}
