namespace Tajer.DAL.Models
{
    public class BaseEntity<TK>
    {
        public TK Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
