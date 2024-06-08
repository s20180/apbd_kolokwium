using apbd_kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_kolokwium.EfConfigurations;

public class SubscriptionEfConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.IdSubscription);
        builder.Property(s => s.IdSubscription).ValueGeneratedOnAdd();

        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Property(s => s.RenewalPeriod).IsRequired();
        builder.Property(s => s.EndTime).IsRequired();
        builder.Property(s => s.Price).IsRequired().HasColumnType("money");
    }
}