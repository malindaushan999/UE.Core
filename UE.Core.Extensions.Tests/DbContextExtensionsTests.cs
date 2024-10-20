using Microsoft.EntityFrameworkCore;
using UE.Core.Extensions.Tests.Mocks;

namespace UE.Core.Extensions.Tests;

/// <summary>
/// Unit tests for DbContextExtensions methods.
/// </summary>
public class DbContextExtensionsTests : IDisposable
{
    private readonly DbContextOptions<TestDbContext> _options;
    private readonly TestDbContext _context;

    public DbContextExtensionsTests()
    {
        _options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "UE.Common.Extensions.TestDatabase")
            .Options;

        _context = new TestDbContext(_options);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    /// <summary>
    /// Tests the RemoveAll method to ensure it removes matching entities.
    /// </summary>
    [Fact]
    public void RemoveAll_RemovesMatchingEntities()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
        };
        _context.AddRange(entities);
        _context.SaveChanges();

        // Act
        _context.RemoveAll<TestEntity>(e => e.Id == 2);
        _context.SaveChanges();

        // Assert
        var remainingEntities = _context.Set<TestEntity>().ToList();
        Assert.Equal(2, remainingEntities.Count);
        Assert.DoesNotContain(remainingEntities, e => e.Id == 2);
    }

    /// <summary>
    /// Tests the RemoveAllAsync method to ensure it removes matching entities asynchronously.
    /// </summary>
    [Fact]
    public async Task RemoveAllAsync_RemovesMatchingEntitiesAsync()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
        };
        _context.AddRange(entities);
        await _context.SaveChangesAsync();

        // Act
        await _context.RemoveAllAsync<TestEntity>(e => e.Id == 2);
        await _context.SaveChangesAsync();

        // Assert
        var remainingEntities = await _context.Set<TestEntity>().ToListAsync();
        Assert.Equal(2, remainingEntities.Count);
        Assert.DoesNotContain(remainingEntities, e => e.Id == 2);
    }

    /// <summary>
    /// Tests the RemoveRangeAsync method to ensure it removes entities asynchronously.
    /// </summary>
    [Fact]
    public async Task RemoveRangeAsync_RemovesEntitiesAsync()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
        };
        _context.AddRange(entities);
        await _context.SaveChangesAsync();

        // Act
        await _context.RemoveRangeAsync(entities);
        await _context.SaveChangesAsync();

        // Assert
        var remainingEntities = await _context.Set<TestEntity>().ToListAsync();
        Assert.Empty(remainingEntities);
    }

    /// <summary>
    /// Tests the RemoveAsync method to ensure it removes a matching entity asynchronously.
    /// </summary>
    [Fact]
    public async Task RemoveAsync_RemovesMatchingEntityAsync()
    {
        // Arrange
        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" },
            new() { Id = 3, Name = "Entity3" },
        };
        _context.AddRange(entities);
        await _context.SaveChangesAsync();

        // Act
        await _context.RemoveAsync<TestEntity>(e => e.Id == 2);
        await _context.SaveChangesAsync();

        // Assert
        var remainingEntities = await _context.Set<TestEntity>().ToListAsync();
        Assert.Equal(2, remainingEntities.Count);
        Assert.DoesNotContain(remainingEntities, e => e.Id == 2);
    }
}
