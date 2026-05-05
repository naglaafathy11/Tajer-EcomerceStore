using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tajer.DAL.Configrations
{
    internal class WishListConfigrations: IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {

            builder.Property(sci => sci.CreatedAt).HasColumnName("DateAdded")
            .HasDefaultValueSql("GETUTCDATE()");
            builder.Ignore(b => b.Name);
            builder.HasKey(w => w.Id);
            builder.Property(o => o.UserId).IsRequired();

            builder.HasOne<ApplicationUser>()
                .WithMany(u => u.Wishlis)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Product>()
                .WithMany(p => p.Wishlis)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
