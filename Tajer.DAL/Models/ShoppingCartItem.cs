namespace Tajer.DAL.Models
{
    public class ShoppingCartItem : BaseEntity<int>
    {
        public string UserId { get; set; } = null!; //FK
        public int ProductId { get; set; } //FK
        public int Quantity { get; set; }


    }
}
