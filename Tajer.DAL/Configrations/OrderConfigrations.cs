namespace Tajer.DAL.Configrations
{
    internal class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.PaymentMethod).IsRequired();
            builder.Property(o => o.PaymentStatus).IsRequired();
            builder.Property(o=>o.UserId).IsRequired();
            builder.Ignore(o => o.Name);

            builder.HasOne<ApplicationUser>()
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
