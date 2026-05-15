namespace Tajer.DAL.Models
{
    public class OrderItem: BaseEntity<int>
    {
        public int OrderId { get; set; } //FK
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; } //FK
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
