using Microsoft.AspNetCore.Identity;

namespace Tajer.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ProfilePicture { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
        public ICollection<Wishlist> Wishlis { get; set; } = null!;


    }
}
