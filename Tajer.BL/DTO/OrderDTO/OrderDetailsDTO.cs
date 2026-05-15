using System;
using System.Collections.Generic;
using System.Text;
using Tajer.DAL.Enums;

namespace Tajer.BL.DTO.OrderDTO
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public Status Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Notes { get; set; }

        public string ShippingStreet { get; set; } = null!;

        public string ShippingCity { get; set; } = null!;

        public string ShippingCountry { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public ICollection<OrderItemDTO> Items { get; set; }
            = new List<OrderItemDTO>();
    }
}
