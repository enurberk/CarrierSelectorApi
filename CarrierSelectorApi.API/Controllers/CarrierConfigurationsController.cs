using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.Business.Concrete;
using CarrierSelectorApi.Entities.DTOs.CarrierConfigurationDTOs;
using CarrierSelectorApi.Entities.DTOs.CarrierDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarrierSelectorApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationsController : Controller
    {
        private readonly ICarrierConfigurationService _carrierConfigurationService;

        public CarrierConfigurationsController(ICarrierConfigurationService carrierConfigurationService)
        {
            _carrierConfigurationService = carrierConfigurationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarrierConfigurations()
        {
            var result = await _carrierConfigurationService.GetAllCarrierConfigurationsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarrierConfiguration([FromBody] CarrierConfigurationCreateDto carrierConfigDto)
        {
            var resultMessage = await _carrierConfigurationService.AddCarrierConfigurationAsync(carrierConfigDto);
            return Ok(resultMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrierConfiguration(int id, [FromBody] CarrierConfigurationUpdateDto carrierConfigDto)
        {
            var resultMessage = await _carrierConfigurationService.UpdateCarrierConfigurationAsync(carrierConfigDto);
            return Ok(resultMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrierConfiguration(int id)
        {
            var resultMessage = await _carrierConfigurationService.DeleteCarrierConfigurationAsync(id);
            return Ok(resultMessage);
        }
    }
}
