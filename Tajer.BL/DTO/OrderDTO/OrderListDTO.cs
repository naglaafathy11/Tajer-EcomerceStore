using System;
using System.Collections.Generic;
using System.Text;
using Tajer.DAL.Enums;

namespace Tajer.BL.DTO.OrderDTO
{
    public class OrderListDTO
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public Status Status { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string UserEmail { get; set; } = null!;
    }
}
