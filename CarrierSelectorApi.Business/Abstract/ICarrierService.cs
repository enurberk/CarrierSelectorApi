using CarrierSelectorApi.Entities.DTOs.CarrierDTOs;
using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.Abstract
{
    public interface ICarrierService
    {
        Task<IEnumerable<CarrierDto>> GetAllCarriersAsync();
        Task<CarrierDto> GetCarrierByIdAsync(int id);
        Task<string> AddCarrierAsync(CarrierCreateDto carrier);
        Task<string> UpdateCarrierAsync(CarrierUpdateDto carrier);
        Task<string> DeleteCarrierAsync(int id);
    }
}
