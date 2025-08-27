using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController(IMediator mediator, ILogger<PropertiesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<PropertiesController> _logger = logger;

        /// <summary>
        /// Method to create a new property
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
        {
            _logger.LogInformation("Creating a new property with address: {Address}", dto.Address);
            var command = new CreatePropertyCommand(dto);
            var id = await _mediator.Send(command);
            _logger.LogInformation("Property created with ID: {Id}", id);

            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }
    }
}