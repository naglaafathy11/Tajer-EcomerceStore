using System;
using System.Collections.Generic;
using System.Text;
using Tajer.DAL.Enums;

namespace Tajer.BL.DTO.OrderDTO
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductImage { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
