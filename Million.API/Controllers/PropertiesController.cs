using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Features.Properties.Commands.ChangePropertyPrice;
using Million.Application.Features.Properties.Commands.CreateProperty;
using Million.Application.Features.Properties.DTOs;

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
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch("{propertyId}/Price")]
        [Authorize(Roles = "Admin,Agent")]
        [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePrice(int propertyId, [FromBody] ChangePropertyPriceDto dto)
        {
            _logger.LogInformation("Changing price for property ID: {PropertyId} to new price: {NewPrice}", propertyId, dto.NewPrice);
            var command = new ChangePropertyPriceCommand(propertyId, dto.NewPrice);
            await _mediator.Send(command);
            _logger.LogInformation("Price changed successfully for property ID: {PropertyId}", propertyId);

            return NoContent();
        }
    }
}