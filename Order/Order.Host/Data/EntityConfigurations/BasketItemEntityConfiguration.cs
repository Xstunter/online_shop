using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations
{
    public class BasketItemEntityConfiguration
    : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable("BasketItems");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .UseHiLo("order_items_hilo")
                .IsRequired();

            builder.Property(ci => ci.OrderHistoryId)
                   .IsRequired(true);

            builder.Property(ci => ci.ItemId)
               .IsRequired(true);

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Price)
                .IsRequired(true);

            builder.Property(ci => ci.Amount)
                .IsRequired(true);
        }
    }
}
