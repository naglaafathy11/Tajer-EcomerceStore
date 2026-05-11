
namespace Tajer.DAL.Configrations
{
    public class ProducctConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.ToTable("Products", t =>
            {
                t.HasCheckConstraint("CK_Product_Price", "[Price] > 0");
            });
            builder.Property(p => p.Description).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(p => p.IsActive).IsRequired();


           

            builder.HasOne(p => p.Category)
       .WithMany(c => c.Products)
       .HasForeignKey(p => p.CategoryId)
       .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
