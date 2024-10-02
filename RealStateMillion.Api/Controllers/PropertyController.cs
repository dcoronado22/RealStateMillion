using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateMillion.Application.Interfaces;
using RealEstateMillion.Domain.Entities;

namespace RealEstateMillion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetAllProperties()
        {
            var properties = await _propertyService.GetAllPropertiesAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(Guid id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<Property>> CreateProperty([FromBody] Property property)
        {
            var createdProperty = await _propertyService.CreatePropertyAsync(property);
            return CreatedAtAction(nameof(GetProperty), new { id = createdProperty.IdProperty }, createdProperty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] Property property)
        {
            if (id != property.IdProperty)
            {
                return BadRequest();
            }

            await _propertyService.UpdatePropertyAsync(property);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            await _propertyService.DeletePropertyAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/images")]
        public async Task<IActionResult> AddImageToProperty(Guid id, [FromBody] string imageUrl)
        {
            await _propertyService.AddImageToPropertyAsync(id, imageUrl);
            return NoContent();
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> ChangePrice(Guid id, [FromBody] decimal newPrice)
        {
            await _propertyService.ChangePriceAsync(id, newPrice);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesWithFilters([FromQuery] PropertyFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var properties = await _propertyService.GetPropertiesWithFiltersAsync(filter);
            return Ok(properties);
        }
    }
}