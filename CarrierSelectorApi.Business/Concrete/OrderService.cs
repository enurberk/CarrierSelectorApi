using AutoMapper;
using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.DataAccess.Abstract;
using CarrierSelectorApi.DataAccess.Concrete;
using CarrierSelectorApi.Entities.DTOs.OrderDTOs;
using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarrierConfigurationRepository _carrierConfigRepository;
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICarrierConfigurationRepository carrierConfigRepository,
            ICarrierRepository carrierRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _carrierConfigRepository = carrierConfigRepository;
            _carrierRepository = carrierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
        /*
        public async Task<string> AddOrderAsync1(OrderCreateDto orderDto)
        {
            var orderEntity = _mapper.Map<Order>(orderDto);

            if (orderEntity.OrderDesi <= 0)
            {
                return "Hata: Sipariş desi değeri 0 veya negatif olamaz!";
            }

            var carrierConfigs = await _carrierConfigRepository.GetAllAsync();
            var carriers = await _carrierRepository.GetAllAsync();

            var validCarriers = carrierConfigs
                .Where(c => orderEntity.OrderDesi >= c.CarrierMinDesi
                        && orderEntity.OrderDesi <= c.CarrierMaxDesi)
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
        */

        public async Task<string> AddOrderAsync(OrderCreateDto orderDto)
        {
            var orderEntity = _mapper.Map<Order>(orderDto);

            if (orderEntity.OrderDesi <= 0)
            {
                return "Hata: Sipariş desi değeri 0 veya negatif olamaz!";
            }

            var carrierConfigs = await _carrierConfigRepository.GetAllAsync();
            var carriers = await _carrierRepository.GetAllAsync();

            var validCarriers = carrierConfigs
                    .Join(carriers,
                          config => config.CarrierId,
                          carrier => carrier.CarrierId,
                            (config, carrier) => new { config, carrier })
                    .Where(c => c.carrier != null
                           && c.carrier.CarrierIsActive
                           && orderEntity.OrderDesi >= c.config.CarrierMinDesi
                           && orderEntity.OrderDesi <= c.config.CarrierMaxDesi)
                    .ToList();

            decimal calculatedCost = 0;
            int selectedCarrierId;

            if (validCarriers.Any())
            {
                var bestCarrier = validCarriers.First();
                calculatedCost = bestCarrier.config.CarrierCost;
                selectedCarrierId = bestCarrier.config.CarrierId;
            }
            else
            {
                var closestCarrier = carrierConfigs
                    .Join(carriers,
                          config => config.CarrierId,
                          carrier => carrier.CarrierId,
                            (config, carrier) => new { config, carrier })
                    .Where(c => c.carrier != null
                           && c.carrier.CarrierIsActive
                    )
                    .Select(c => new
                    {
                    Config = c,
                    CarrierId = c.carrier.CarrierId,
                    ExtraCost = (orderEntity.OrderDesi > c.config.CarrierMaxDesi)
                    ? (orderEntity.OrderDesi - c.config.CarrierMaxDesi) * c.carrier.CarrierPlusDesiCost
                    : 0,
                    TotalCost = c.config.CarrierCost + ((orderEntity.OrderDesi > c.config.CarrierMaxDesi)
                    ? (orderEntity.OrderDesi - c.config.CarrierMaxDesi) * c.carrier.CarrierPlusDesiCost
                    : 0) 
                    })
                    .OrderBy(c => c.TotalCost) 
                    .FirstOrDefault();

                var alternativeCarrier = carrierConfigs
                     .Join(carriers,
                          config => config.CarrierId,
                          carrier => carrier.CarrierId,
                            (config, carrier) => new { config, carrier })
                    .Where(c => orderEntity.OrderDesi < c.config.CarrierMinDesi
                           && c.carrier != null
                           && c.carrier.CarrierIsActive)
                    .Select(c => new
                    {
                        Config = c,
                        CarrierId = c.carrier.CarrierId,
                        ExtraCost = 0,
                        TotalCost = c.config.CarrierCost
                    })
                    .Where(c => c.TotalCost < closestCarrier.TotalCost)
                    .OrderBy(c => c.TotalCost)
                    .FirstOrDefault();

                if (alternativeCarrier != null)
                {
                    closestCarrier = alternativeCarrier;
                }

                calculatedCost = closestCarrier.TotalCost;
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