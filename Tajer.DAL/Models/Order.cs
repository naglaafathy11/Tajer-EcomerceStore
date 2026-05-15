using Tajer.DAL.Enums;

namespace Tajer.DAL.Models
{
    public class Order : BaseEntity<int>
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingStreet { get; set; } = null!;
        public string ShippingCity { get; set; } = null!;
        public string ShippingCountry { get; set; } = null!;


        public Status Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Notes { get; set; }
        public string   UserId { get; set; }  = null!; //FK
        public ApplicationUser User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = null!;
    }
}
