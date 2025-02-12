using CarrierSelectorApi.Entities.DTOs.OrderDTOs;
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
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<string> AddOrderAsync(OrderCreateDto order);
        Task<string> DeleteOrderAsync(int id);
    }
}
