namespace Tajer.PL.ViewModels
{
    public class ProductCardVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string ImageUrl { get; set; }
        public double AverageRating { get; set; }
        public bool IsInWishlist { get; set; }
    }
}
