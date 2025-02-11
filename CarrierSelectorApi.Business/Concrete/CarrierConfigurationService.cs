using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.DataAccess.Abstract;
using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.Concrete
{
    public class CarrierConfigurationService : ICarrierConfigurationService
    {
        private readonly ICarrierConfigurationRepository _carrierConfigRepository;

        public CarrierConfigurationService(ICarrierConfigurationRepository carrierConfigRepository)
        {
            _carrierConfigRepository = carrierConfigRepository;
        }

        public async Task<IEnumerable<CarrierConfiguration>> GetAllCarrierConfigurationsAsync()
        {
            return await _carrierConfigRepository.GetAllAsync();
        }

        public async Task<string> AddCarrierConfigurationAsync(CarrierConfiguration config)
        {
            await _carrierConfigRepository.AddAsync(config);
            return "Kargo firması konfigürasyonu eklendi.";
        }

        public async Task<string> UpdateCarrierConfigurationAsync(CarrierConfiguration config)
        {
            await _carrierConfigRepository.UpdateAsync(config);
            return "Kargo firması konfigürasyonu güncellendi.";
        }

        public async Task<string> DeleteCarrierConfigurationAsync(int id)
        {
            await _carrierConfigRepository.DeleteAsync(id);
            return $"{id} ID'li kargo firması konfigürasyonu silindi.";
        }
    }
}
