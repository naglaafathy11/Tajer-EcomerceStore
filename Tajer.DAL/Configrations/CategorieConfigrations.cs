using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tajer.DAL.Configrations
{
    public class CategorieConfigrations: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(200)").IsRequired(false);
            
        }
    }
}
