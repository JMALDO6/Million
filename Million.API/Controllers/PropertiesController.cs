using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Features.Properties.Commands.ChangePropertyPrice;
using Million.Application.Features.Properties.Commands.CreateProperty;
using Million.Application.Features.Properties.Commands.UpdateProperty;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Features.Properties.Queries.GetProperties;

namespace Million.API.Controllers
{
    /// <summary>
    /// Controller to add actions in property entity
    /// </summary>
    /// <remarks>
    /// Ctor
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController(IMediator mediator, ILogger<PropertiesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<PropertiesController> _logger = logger;

        /// <summary>
        /// Method to create a new property
        /// </summary>
        /// <param name="createPropertyDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Agent")]
        [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto createPropertyDto)
        {
            _logger.LogInformation("Creating a new property with address: {Address}", createPropertyDto.Address);
            var command = new CreatePropertyCommand(createPropertyDto);
            var propertyDto = await _mediator.Send(command);
            _logger.LogInformation("Property created with ID: {Id}", propertyDto);

            return CreatedAtAction(nameof(Create), new { propertyDto }, new { propertyDto });
        }

        /// <summary>
        /// Patch method to change the price of a property
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="changePropertyPriceDto"></param>
        /// <returns></returns>
        [HttpPatch("{propertyId}/Price")]
        [Authorize(Roles = "Admin,Agent")]
        [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePrice(int propertyId, [FromBody] ChangePropertyPriceDto changePropertyPriceDto)
        {
            _logger.LogInformation("Changing price for property ID: {PropertyId} to new price: {NewPrice}", propertyId, changePropertyPriceDto.NewPrice);
            var command = new ChangePropertyPriceCommand(propertyId, changePropertyPriceDto.NewPrice);
            await _mediator.Send(command);
            _logger.LogInformation("Price changed successfully for property ID: {PropertyId}", propertyId);

            return NoContent();
        }

        /// <summary>
        /// Update an existing property
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="updatePropertyDto"></param>
        /// <returns></returns>
        [HttpPut("{propertyId}")]
        [Authorize(Roles = "Admin,Agent")]
        [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProperty(int propertyId, [FromBody] UpdatePropertyDto updatePropertyDto)
        {
            _logger.LogInformation("Updating property with ID: {PropertyId}", propertyId);
            var command = new UpdatePropertyCommand(propertyId, updatePropertyDto);
            var propertyDto = await _mediator.Send(command);
            _logger.LogInformation("Property with ID: {PropertyId} updated successfully", propertyId);

            return CreatedAtAction(nameof(UpdateProperty), new { propertyDto }, new { propertyDto });
        }

        /// <summary>
        /// Get properties with optional filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Agent,User")]
        [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProperties([FromQuery] PropertyFilterDto filters)
        {
            _logger.LogInformation("Retrieving properties with filters: {@Filters}", filters);
            var query = new GetPropertiesQuery(filters);
            var result = await _mediator.Send(query);
            _logger.LogInformation("Successfully retrieved properties");

            return Ok(result);
        }
    }
}