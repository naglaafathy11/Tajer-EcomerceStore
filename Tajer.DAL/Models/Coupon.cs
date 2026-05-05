using Tajer.DAL.Enums;

namespace Tajer.DAL.Models
{
    public class Coupon : BaseEntity<int>
    {
        public string Code { get; set; } = null!;
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal MinOrderAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
