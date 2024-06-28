using AllegroBillings.Data.Models;
using Microsoft.EntityFrameworkCore;

public class BillingContext : DbContext
{
    public BillingContext(DbContextOptions<BillingContext> options) : base(options)
    {
    }

    public DbSet<Billing> Billings { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<OrderTable> OrderTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderTable>()
            .HasIndex(o => o.Id)
            .IsUnique();

        modelBuilder.Entity<Billing>()
            .OwnsOne(b => b.Type);

        modelBuilder.Entity<Billing>()
            .OwnsOne(b => b.Value);

        modelBuilder.Entity<Billing>()
            .OwnsOne(b => b.Tax);

        modelBuilder.Entity<Billing>()
            .OwnsOne(b => b.Balance);

        base.OnModelCreating(modelBuilder);
    }
}