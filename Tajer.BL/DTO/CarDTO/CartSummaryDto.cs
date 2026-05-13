using System;
using System.Collections.Generic;
using System.Text;

namespace Tajer.BL.DTO.CarDTO
{
    public class CartSummaryDto
    {
        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal Total { get; set; }
    }
}
