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
        Task<IEnumerable<Carrier>> GetAllCarriersAsync();
        Task<Carrier> GetCarrierByIdAsync(int id);
        Task<string> AddCarrierAsync(Carrier carrier);
        Task<string> UpdateCarrierAsync(Carrier carrier);
        Task<string> DeleteCarrierAsync(int id);
    }
}
