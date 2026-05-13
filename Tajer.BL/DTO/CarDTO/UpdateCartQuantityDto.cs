using System;
using System.Collections.Generic;
using System.Text;

namespace Tajer.BL.DTO.CarDTO
{
    public class UpdateCartQuantityDto
    {
        public int ProductId { get; set; }
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
