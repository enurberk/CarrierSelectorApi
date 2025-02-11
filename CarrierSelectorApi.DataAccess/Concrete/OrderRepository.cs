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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<IEnumerable<Order>> GetOrdersByCarrierIdAsync(int carrierId)
        //{
        //    return await _context.Orders.Where(o => o.CarrierId == carrierId).ToListAsync();
        //}
    }
}
