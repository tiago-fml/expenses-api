using expenses_api.Enums;
using expenses_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace expenses_api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasOne(x=>x.User)
                .WithMany()
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(t => t.Type)
                .HasConversion(new EnumToStringConverter<TransactionType>())
                .IsRequired();
            
            entity.Property(t => t.ExecutedAt)
                .HasConversion(
                    v => v.ToUniversalTime(),                  // Save as UTC
                    v => DateTime.SpecifyKind(v.UtcDateTime, DateTimeKind.Utc) // Read as UTC
                );
        });
        
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasOne(x=>x.User)
                .WithMany()
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(t => t.Type)
                .HasConversion(new EnumToStringConverter<TransactionType>())
                .IsRequired();
        });
    }
}