using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using resisafe.Models;

namespace resisafe.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    /// <summary>
    /// Configures foreign key delete behavior for the Booking entity.
    /// Both relationships (Guest and Property) are set to Restrict rather
    /// than Cascade, because SQL Server rejects multiple cascade paths that
    /// converge on the same AspNetUsers table (Booking -> Guest, and
    /// Booking -> Property -> Owner).
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Booking>()
            .HasOne(b => b.Guest)
            .WithMany()
            .HasForeignKey(b => b.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Booking>()
            .HasOne(b => b.Property)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
