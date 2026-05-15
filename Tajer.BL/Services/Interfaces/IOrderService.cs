using System;
using System.Collections.Generic;
using System.Text;
using Tajer.BL.DTO.OrderDTO;
using Tajer.DAL.Models;

namespace Tajer.BL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderListDTO>> GetUserOrdersAsync(string userId);
        Task<OrderDetailsDTO> GetOrderByIdAsync(int orderId, string userId);

        // Admin Orders
        Task<IEnumerable<OrderListDTO>> GetAllOrdersForAdminAsync(string? status, string? search);

        // Create Order
        Task<OrderDetailsDTO> PlaceOrderAsync( string userId, CreateOrderDTO dto);

        // Update Status
        Task UpdateOrderStatusAsync(int orderId,UpdateOrderStatusDTO dto);

        // User Cancel
        Task CancelOrderAsync(int orderId, string userId);

        // Admin Cancel
        Task AdminCancelOrderAsync(int orderId);
    }
}
