namespace Tajer.DAL.Models
{
    public class Wishlist : BaseEntity<int>
    {
        public string UserId { get; set; } = null!; //FK
        public int ProductId { get; set; } //FK

    }
}
