using apbd_kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apbd_kolokwium.EfConfigurations;

public class ClientEfConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.IdClient);
        builder.Property(c => c.IdClient).ValueGeneratedOnAdd();

        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Phone).IsRequired(false).HasMaxLength(100);
    }
}