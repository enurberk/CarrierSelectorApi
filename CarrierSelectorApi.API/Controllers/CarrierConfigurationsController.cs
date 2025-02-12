using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.Entities.DTOs.CarrierConfigurationDTOs;
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
            await _carrierConfigurationService.AddCarrierConfigurationAsync(carrierConfigDto);
            return Ok("Kargo firması konfigürasyonu başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrierConfiguration(int id, [FromBody] CarrierConfigurationUpdateDto carrierConfigDto)
        {
            await _carrierConfigurationService.UpdateCarrierConfigurationAsync(carrierConfigDto);
            return Ok("Kargo firması konfigürasyonu başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrierConfiguration(int id)
        {
            await _carrierConfigurationService.DeleteCarrierConfigurationAsync(id);
            return Ok("Kargo firması konfigürasyonu başarıyla silindi.");
        }
    }
}
