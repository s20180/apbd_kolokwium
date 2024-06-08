using apbd_kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_kolokwium.EfConfigurations;

public class SaleEfConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(s => s.IdSale);
        builder.Property(s => s.IdSale).ValueGeneratedOnAdd();

        builder.Property(s => s.CreatedAt).IsRequired();
        
        builder.HasOne<Subscription>(d => d.IdSubscriptionNavigation)
            .WithMany(s => s.Sales)
            .HasForeignKey(d => d.IdSubscription);
        
        builder.HasOne<Client>(d => d.IdClientNavigation)
            .WithMany(s => s.Sales)
            .HasForeignKey(d => d.IdClient);
    }
}