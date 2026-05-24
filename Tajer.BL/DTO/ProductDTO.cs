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
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }      // ← nullable

        public string? AddedAt { get; set; }       // ← nullable
        public string? UpdatedAt { get; set; }
        public string? SKU { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }  // ← nullable

        public List<string> Reviews { get; set; } = new();



    }
}
