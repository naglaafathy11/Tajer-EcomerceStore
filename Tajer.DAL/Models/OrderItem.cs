namespace Tajer.DAL.Models
{
    public class OrderItem: BaseEntity<int>
    {
        public int OrderId { get; set; } //FK
        public int ProductId { get; set; } //FK
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
