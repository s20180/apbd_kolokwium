using apbd_kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_kolokwium.EfConfigurations;

public class PaymentEfConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.IdPayment);
        builder.Property(p => p.IdPayment).ValueGeneratedOnAdd();

        builder.Property(p => p.Date).IsRequired();
        
        builder.HasOne<Subscription>(d => d.IdSubscriptionNavigation)
            .WithMany(s => s.Payments)
            .HasForeignKey(d => d.IdSubscription);
        
        builder.HasOne<Client>(d => d.IdClientNavigation)
            .WithMany(s => s.Payments)
            .HasForeignKey(d => d.IdClient);
    }
}