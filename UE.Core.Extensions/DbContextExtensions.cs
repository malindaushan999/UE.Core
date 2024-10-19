using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UE.Core.Extensions;

/// <summary>
/// Provides extension methods for the Entity Framework Core DbContext to work with database schemas.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Creates a database schema with the specified name if it does not already exist.
    /// </summary>
    /// <param name="context">The DbContext instance to which this extension method applies.</param>
    /// <param name="schemaName">The name of the schema to create.</param>
    public static void CreateSchema(this DbContext context, string schemaName)
    {
        // Execute a SQL command to create the schema
        context.Database.ExecuteSql($"CREATE SCHEMA IF NOT EXISTS {schemaName}");
    }

    /// <summary>
    /// Removes all entities of the specified type that match the given condition from the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to remove.</typeparam>
    /// <param name="context">The DbContext instance.</param>
    /// <param name="predicate">A predicate specifying the condition for selecting entities to remove.</param>
    public static void RemoveAll<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        var entities = context.Set<TEntity>().Where(predicate);

        foreach (var entity in entities)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        context.SaveChanges();
    }

    /// <summary>
    /// Removes all entities of the specified type that match the given condition from the database asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to remove.</typeparam>
    /// <param name="context">The DbContext instance.</param>
    /// <param name="predicate">A predicate specifying the condition for selecting entities to remove.</param>
    public static async Task RemoveAllAsync<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        var entities = await context.Set<TEntity>().Where(predicate).ToListAsync();

        foreach (var entity in entities)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Removes a range of entities from the database asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to remove.</typeparam>
    /// <param name="context">The DbContext instance.</param>
    /// <param name="entities">The collection of entities to remove.</param>
    public static async Task RemoveRangeAsync<TEntity>(this DbContext context, IEnumerable<TEntity> entities) where TEntity : class
    {
        context.Set<TEntity>().RemoveRange(entities);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Removes an entity of the specified type that matches the given condition from the database asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to remove.</typeparam>
    /// <param name="context">The DbContext instance.</param>
    /// <param name="predicate">A predicate specifying the condition for selecting the entity to remove.</param>
    public static async Task RemoveAsync<TEntity>(this DbContext context, Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        var entity = await context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        if (entity != null)
        {
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
