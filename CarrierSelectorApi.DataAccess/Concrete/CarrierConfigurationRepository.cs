using CarrierSelectorApi.DataAccess.Abstract;
using CarrierSelectorApi.DataAccess.Context;
using CarrierSelectorApi.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.DataAccess.Concrete
{
    public class CarrierConfigurationRepository : GenericRepository<CarrierConfiguration>, ICarrierConfigurationRepository
    {
        public CarrierConfigurationRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<CarrierConfiguration> GetByDesiRangeAsync(int carrierId, int desi)
        //{
        //    return await _context.CarrierConfigurations
        //        .Where(c => c.CarrierId == carrierId && c.CarrierMinDesi <= desi && c.CarrierMaxDesi >= desi)
        //        .FirstOrDefaultAsync();
        //}
    }
}
