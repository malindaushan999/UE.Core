using Microsoft.EntityFrameworkCore;
using UE.Core.Common.Entities;

namespace UE.Core.Common.Tests.Mocks;

public class MockDbContext : DbContext
{
    public MockDbContext(DbContextOptions<MockDbContext> options) : base(options)
    {

    }

    public DbSet<MockEntity> MockEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Retrieve entries that are added or modified.
        var addedOrModifiedEntries = base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified);

        foreach (var entry in addedOrModifiedEntries)
        {
            // Update modified timestamp and modified by information.
            entry.Entity.ModifiedTimestamp = DateTime.Now;
            entry.Entity.ModifiedBy = 1;

            if (entry.State == EntityState.Added)
            {
                // For new entities, set created timestamp and created by information.
                entry.Entity.CreatedTimestamp = DateTime.Now;
                entry.Entity.CreatedBy = 1;
            }
        }

        // Retrieve entries that are marked for deletion.
        var deletedEntries = base.ChangeTracker.Entries<BaseEntity>()
            .Where(x => x.State == EntityState.Deleted);

        foreach (var entry in deletedEntries)
        {
            // Set the IsDeleted flag and update timestamps for soft delete.
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedTimestamp = DateTime.Now;
            entry.Entity.DeletedBy = 1;

            // Change the state to Modified so that EF Core updates the entity accordingly.
            entry.State = EntityState.Modified;
        }

        // Save changes to the database and return the number of affected rows.
        return base.SaveChangesAsync(cancellationToken);
    }
}
