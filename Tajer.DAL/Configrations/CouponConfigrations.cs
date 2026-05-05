using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tajer.DAL.Configrations
{
    public class CouponConfigrations : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
            builder.HasIndex(c => c.Code).IsUnique();
            
            builder.Property(c => c.IsActive).IsRequired();
          


        }
    }
}
