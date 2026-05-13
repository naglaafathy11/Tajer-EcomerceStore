using System;
using System.Collections.Generic;
using System.Text;

namespace Tajer.BL.DTO.CarDTO
{
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; } = null!;
    }
}
