using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tajer.DAL.Configrations
{
    internal class ReviewConfigrations: IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Rating).IsRequired();
            builder.Property(r => r.Comment).IsRequired(false);
            builder.Property(r => r.IsApproved).IsRequired();
            builder.Property(o => o.UserId).IsRequired();

            builder.Ignore(b => b.Name);

            builder.HasOne<ApplicationUser>()
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Product>()
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
