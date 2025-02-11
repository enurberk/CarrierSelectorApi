using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.Abstract
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<string> AddOrderAsync(Order order);
        Task<string> DeleteOrderAsync(int id);
    }
}
