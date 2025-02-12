using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.Business.Concrete;
using CarrierSelectorApi.Entities.DTOs.CarrierDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarrierSelectorApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : Controller
    {
        private readonly ICarrierService _carrierService;

        public CarriersController(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarriers()
        {
            var result = await _carrierService.GetAllCarriersAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarrier([FromBody] CarrierCreateDto carrierDto)
        {
            var resultMessage = await _carrierService.AddCarrierAsync(carrierDto);
            return Ok(resultMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrier(int id, [FromBody] CarrierUpdateDto carrierDto)
        {
            var resultMessage = await _carrierService.UpdateCarrierAsync(carrierDto);
            return Ok(resultMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrier(int id)
        {
            var resultMessage = await _carrierService.DeleteCarrierAsync(id);
            return Ok(resultMessage);
        }
    }
}
