namespace Tajer.DAL.Models
{
    public class Category : BaseEntity<int>
    {
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } 
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<Product> Products { get; set; } = null!;
    }
}


