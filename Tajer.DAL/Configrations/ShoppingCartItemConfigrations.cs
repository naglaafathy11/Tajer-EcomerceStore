using Microsoft.EntityFrameworkCore;

namespace Tajer.DAL.Configrations
{
    public class ShoppingCartItemConfigrations : IEntityTypeConfiguration<ShoppingCartItem>

    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(sci => sci.Id);

            builder.Property(sci => sci.Quantity).IsRequired();
            builder.Property(sci => sci.CreatedAt).HasColumnName("DateAdded")
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();
            builder.Ignore(b => b.UpdatedAt);
            builder.Ignore(b => b.Name);
            builder.HasOne<ApplicationUser>()
                .WithMany(au => au.ShoppingCartItems)
                .HasForeignKey(sci => sci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Product>()
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(sci => sci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
