using AutoMapper;
using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.DataAccess.Abstract;
using CarrierSelectorApi.Entities.DTOs.CarrierDTOs;
using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.Concrete
{
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;

        public CarrierService(ICarrierRepository carrierRepository, IMapper mapper)
        {
            _carrierRepository = carrierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarrierDto>> GetAllCarriersAsync()
        {
            var carriers = await _carrierRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CarrierDto>>(carriers);
        }

        public async Task<CarrierDto> GetCarrierByIdAsync(int id)
        {
            var carrier = await _carrierRepository.GetByIdAsync(id);
            return _mapper.Map<CarrierDto>(carrier);
        }

        public async Task<string> AddCarrierAsync(CarrierCreateDto carrierDto)
        {
            var carrierEntity = _mapper.Map<Carrier>(carrierDto);
            await _carrierRepository.AddAsync(carrierEntity);
            return "Kargo firması başarıyla eklendi.";
        }

        public async Task<string> UpdateCarrierAsync(CarrierUpdateDto carrierDto)
        {
            var carrierEntity = _mapper.Map<Carrier>(carrierDto);
            await _carrierRepository.UpdateAsync(carrierEntity);
            return "Kargo firması başarıyla güncellendi.";
        }

        public async Task<string> DeleteCarrierAsync(int id)
        {
            await _carrierRepository.DeleteAsync(id);
            return $"{id} ID'li kargo firması silindi.";
        }
    }
}
