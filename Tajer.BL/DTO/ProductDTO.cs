using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tajer.BL.DTO
{
    public class ProductDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsActive { get; set; }
        public string SKU { get; set; } = null!;

        public string AddedAt { get; set; } = null!;
        public string? UpdatedAt { get; set; } 




    }
}
