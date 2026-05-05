namespace Tajer.DAL.Models
{
    public class Review : BaseEntity<int>
    {
        public string UserId { get; set; } = null!; //FK
        public int ProductId { get; set; } //FK
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public bool IsApproved { get; set; }

    }
}
