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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarrierConfigurationRepository _carrierConfigRepository;
        private readonly ICarrierRepository _carrierRepository;

        public OrderService(IOrderRepository orderRepository, ICarrierConfigurationRepository carrierConfigRepository, ICarrierRepository carrierRepository)
        {
            _orderRepository = orderRepository;
            _carrierConfigRepository = carrierConfigRepository;
            _carrierRepository = carrierRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<string> AddOrderAsync(Order order)
        {
            var carrierConfigs = await _carrierConfigRepository.GetAllAsync();

            // Sipariş desisi bir kargo firmasının aralığına giriyor mu?
            var validCarriers = carrierConfigs
                .Where(c => order.OrderDesi >= c.CarrierMinDesi && order.OrderDesi <= c.CarrierMaxDesi)
                .OrderBy(c => c.CarrierCost)
                .ToList();

            decimal calculatedCost = 0;
            int selectedCarrierId;

            if (validCarriers.Any())
            {
                var bestCarrier = validCarriers.First();
                calculatedCost = bestCarrier.CarrierCost;
                selectedCarrierId = bestCarrier.CarrierId;
            }
            else
            {
                var closestCarrier = carrierConfigs.OrderBy(c => Math.Abs(order.OrderDesi - c.CarrierMaxDesi)).First();
                int difference = order.OrderDesi - closestCarrier.CarrierMaxDesi;
                calculatedCost = closestCarrier.CarrierCost + (difference * closestCarrier.Carrier.CarrierPlusDesiCost);
                selectedCarrierId = closestCarrier.CarrierId;
            }

            order.OrderCarrierCost = calculatedCost;
            order.CarrierId = selectedCarrierId;
            order.OrderDate = DateTime.UtcNow;

            await _orderRepository.AddAsync(order);
            return $"Sipariş başarıyla oluşturuldu. Seçilen kargo firması: {selectedCarrierId}, Toplam Kargo Ücreti: {calculatedCost}₺";
        }

        public async Task<string> DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return $"{id} ID'li sipariş silindi.";
        }
    }
}
