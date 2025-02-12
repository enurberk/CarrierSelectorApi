﻿using CarrierSelectorApi.Business.Abstract;
using CarrierSelectorApi.Entities.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarrierSelectorApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderCreateDto orderDto)
        {
            await _orderService.AddOrderAsync(orderDto);
            return Ok("Sipariş başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return Ok("Sipariş başarıyla silindi.");
        }
    }
}
