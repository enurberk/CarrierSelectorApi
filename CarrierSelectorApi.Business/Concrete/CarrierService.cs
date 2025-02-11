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
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;

        public CarrierService(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<IEnumerable<Carrier>> GetAllCarriersAsync()
        {
            return await _carrierRepository.GetAllAsync();
        }

        public async Task<Carrier> GetCarrierByIdAsync(int id)
        {
            return await _carrierRepository.GetByIdAsync(id);
        }

        public async Task<string> AddCarrierAsync(Carrier carrier)
        {
            await _carrierRepository.AddAsync(carrier);
            return "Kargo firması başarıyla eklendi.";
        }

        public async Task<string> UpdateCarrierAsync(Carrier carrier)
        {
            await _carrierRepository.UpdateAsync(carrier);
            return "Kargo firması başarıyla güncellendi.";
        }

        public async Task<string> DeleteCarrierAsync(int id)
        {
            await _carrierRepository.DeleteAsync(id);
            return $"{id} ID'li kargo firması silindi.";
        }
    }
}
