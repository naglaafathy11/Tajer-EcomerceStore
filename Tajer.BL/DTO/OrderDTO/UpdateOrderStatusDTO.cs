using System;
using System.Collections.Generic;
using System.Text;
using Tajer.DAL.Enums;

namespace Tajer.BL.DTO.OrderDTO
{
    public class UpdateOrderStatusDTO
    {
        public Status NewStatus { get; set; }

        public string? TrackingNumber { get; set; }
    }
}
