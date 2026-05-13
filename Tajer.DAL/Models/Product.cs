namespace Tajer.DAL.Models
{
    public class Product : BaseEntity<int>
    {
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsActive { get; set; }
        public string SKU { get; set; } = null!;
        public int CategoryId { get; set; } // FK 
        public Category Category { get; set; } = null!;

             public ICollection<OrderItem> OrderItems { get; set; } = null!;
            public ICollection<Review> Reviews { get; set; } = null!;
           public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
            public ICollection<Wishlist> Wishlis { get; set; } = null!;

    }  

}
