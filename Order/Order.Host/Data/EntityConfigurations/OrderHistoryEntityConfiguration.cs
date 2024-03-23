using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;


namespace Order.Host.Data.EntityConfigurations
{
    public class OrderHistoryEntityConfiguration
        : IEntityTypeConfiguration<OrderHistory>
    {
        public void Configure(EntityTypeBuilder<OrderHistory> builder)
        {
            builder.ToTable("Order");

            builder.Property(ci => ci.Id)
                .UseHiLo("order_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.LastName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.TotalPrice)
                .IsRequired(true);
        }
    }
}
