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
        Task<IEnumerable<CarrierConfiguration>> GetAllCarrierConfigurationsAsync();
        Task<string> AddCarrierConfigurationAsync(CarrierConfiguration config);
        Task<string> UpdateCarrierConfigurationAsync(CarrierConfiguration config);
        Task<string> DeleteCarrierConfigurationAsync(int id);
    }
}
