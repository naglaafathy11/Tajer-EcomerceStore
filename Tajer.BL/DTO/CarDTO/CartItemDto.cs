using System;
using System.Collections.Generic;
using System.Text;

namespace Tajer.BL.DTO.CarDTO
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }
    }
}
