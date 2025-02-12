using CarrierSelectorApi.Entities.DTOs.CarrierConfigurationDTOs;
using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.Abstract
{
    public interface ICarrierConfigurationService
    {
        Task<IEnumerable<CarrierConfigurationDto>> GetAllCarrierConfigurationsAsync();
        Task<string> AddCarrierConfigurationAsync(CarrierConfigurationCreateDto config);
        Task<string> UpdateCarrierConfigurationAsync(CarrierConfigurationUpdateDto config);
        Task<string> DeleteCarrierConfigurationAsync(int id);
    }
}
