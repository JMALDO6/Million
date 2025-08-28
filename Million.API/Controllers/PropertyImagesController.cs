using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Features.Properties.Commands.AddPropertyImage;
using Million.Application.Features.Properties.DTOs;

namespace Million.API.Controllers
{
    /// <summary>
    /// PropertyImagesController to add images to properties
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    [ApiController]
    [Route("api/Properties/{idProperty}/[controller]")]
    [ApiVersion("1.0")]
    public class PropertyImagesController(IMediator mediator, ILogger<PropertyImagesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<PropertyImagesController> _logger = logger;

        /// <summary>
        /// Method to add an image to a property
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="addPropertyImageDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Agent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(int idProperty, [FromForm] AddPropertyImageDto addPropertyImageDto)
        {
            _logger.LogInformation("Adding image to property with ID: {IdProperty}", idProperty);
            var command = new AddPropertyImageCommand(idProperty, addPropertyImageDto);
            var propertyImageDto = await _mediator.Send(command);
            _logger.LogInformation("Image added to property with ID: {IdPropertyImage}", propertyImageDto.IdPropertyImage);

            return CreatedAtAction(nameof(UploadImage), new { idProperty, propertyImageDto }, new { propertyImageDto });
        }
    }
}