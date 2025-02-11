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
    public class CarrierRepository : GenericRepository<Carrier>, ICarrierRepository
    {
        public CarrierRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<IEnumerable<Carrier>> GetActiveCarriersAsync()
        //{
        //    return await _context.Carriers.Where(c => c.CarrierIsActive).ToListAsync();
        //}
    }
}
