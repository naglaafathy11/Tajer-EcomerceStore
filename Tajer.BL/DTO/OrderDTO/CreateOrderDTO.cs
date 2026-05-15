using System;
using System.Collections.Generic;
using System.Text;
using Tajer.DAL.Enums;

namespace Tajer.BL.DTO.OrderDTO
{
    public class CreateOrderDTO
    {
        public string ShippingStreet { get; set; } = null!;

        public string ShippingCity { get; set; } = null!;

        public string ShippingCountry { get; set; } = null!;

        public PaymentMethod PaymentMethod { get; set; }

        public string? Notes { get; set; }
    }
}
