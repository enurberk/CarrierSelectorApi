using AutoMapper;
using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.DataAccess.Abstract;
using CarrierSelectorApi.Entities.DTOs.CarrierConfigurationDTOs;
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
        private readonly IMapper _mapper;

        public CarrierConfigurationService(ICarrierConfigurationRepository carrierConfigRepository, IMapper mapper)
        {
            _carrierConfigRepository = carrierConfigRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarrierConfigurationDto>> GetAllCarrierConfigurationsAsync()
        {
            var configs = await _carrierConfigRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CarrierConfigurationDto>>(configs);
        }

        public async Task<string> AddCarrierConfigurationAsync(CarrierConfigurationCreateDto configDto)
        {
            var configEntity = _mapper.Map<CarrierConfiguration>(configDto);
            await _carrierConfigRepository.AddAsync(configEntity);
            return "Kargo firması konfigürasyonu eklendi.";
        }

        public async Task<string> UpdateCarrierConfigurationAsync(CarrierConfigurationUpdateDto configDto)
        {
            var configEntity = _mapper.Map<CarrierConfiguration>(configDto);
            await _carrierConfigRepository.UpdateAsync(configEntity);
            return "Kargo firması konfigürasyonu güncellendi.";
        }

        public async Task<string> DeleteCarrierConfigurationAsync(int id)
        {
            await _carrierConfigRepository.DeleteAsync(id);
            return $"{id} ID'li kargo firması konfigürasyonu silindi.";
        }
    }
}
