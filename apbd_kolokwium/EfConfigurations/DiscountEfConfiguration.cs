using apbd_kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_kolokwium.EfConfigurations;

public class DiscountEfConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(d => d.IdDiscount);
        builder.Property(d => d.IdDiscount).ValueGeneratedOnAdd();

        builder.Property(d => d.Value).IsRequired();
        builder.Property(d => d.DateFrom).IsRequired();
        builder.Property(d => d.DateTo).IsRequired();
        
        builder.HasOne<Subscription>(d => d.IdSubscriptionNavigation)
            .WithMany(s => s.Discounts)
            .HasForeignKey(d => d.IdSubscription);
    }
}