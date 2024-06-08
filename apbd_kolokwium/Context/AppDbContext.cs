using apbd_kolokwium.EfConfigurations;
using apbd_kolokwium.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_kolokwium.Context;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientEfConfiguration());
        modelBuilder.ApplyConfiguration(new DiscountEfConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentEfConfiguration());
        modelBuilder.ApplyConfiguration(new SaleEfConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionEfConfiguration());
    }
}