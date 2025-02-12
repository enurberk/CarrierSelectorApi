using AutoMapper;
using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.DataAccess.Abstract;
using CarrierSelectorApi.Entities.DTOs.OrderDTOs;
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
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICarrierConfigurationRepository carrierConfigRepository,
            ICarrierRepository carrierRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _carrierConfigRepository = carrierConfigRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<string> AddOrderAsync(OrderCreateDto orderDto)
        {
            var orderEntity = _mapper.Map<Order>(orderDto);
            var carrierConfigs = await _carrierConfigRepository.GetAllAsync();

            var validCarriers = carrierConfigs
                .Where(c => orderEntity.OrderDesi >= c.CarrierMinDesi && orderEntity.OrderDesi <= c.CarrierMaxDesi)
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
                var closestCarrier = carrierConfigs.OrderBy(c => Math.Abs(orderEntity.OrderDesi - c.CarrierMaxDesi)).First();
                int difference = orderEntity.OrderDesi - closestCarrier.CarrierMaxDesi;
                calculatedCost = closestCarrier.CarrierCost + (difference * closestCarrier.Carrier.CarrierPlusDesiCost);
                selectedCarrierId = closestCarrier.CarrierId;
            }

            orderEntity.OrderCarrierCost = calculatedCost;
            orderEntity.CarrierId = selectedCarrierId;
            orderEntity.OrderDate = DateTime.UtcNow;

            await _orderRepository.AddAsync(orderEntity);
            return $"Sipariş başarıyla oluşturuldu. Seçilen kargo firması: {selectedCarrierId}, Toplam Kargo Ücreti: {calculatedCost}₺";
        }

        public async Task<string> DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return $"{id} ID'li sipariş silindi.";
        }
    }
}