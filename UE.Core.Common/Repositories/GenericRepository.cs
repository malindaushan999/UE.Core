using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UE.Core.Common.DTOs;
using UE.Core.Common.Entities;
using UE.Core.Common.Interfaces;
using UE.Core.Extensions;

#nullable disable

namespace UE.Core.Common.Repositories;

/// <inheritdoc />
public class GenericRepository<TEntity, TContext, TDto, TQuery> : IGenericRepository<TEntity, TDto, TQuery>
    where TEntity : BaseEntity
    where TContext : DbContext
    where TDto : BaseDto
    where TQuery : IPagination
{
    private readonly TContext _context;

    /// <inheritdoc />
    public GenericRepository(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<int> CreateAsync(TDto dto)
    {
        var entity = await _context.Set<TEntity>().AddAsync(dto.Adapt<TEntity>());
        await _context.SaveChangesAsync();
        return entity.Entity.Id;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAllAsync(List<int> Ids)
    {
        var toRemoveList = _context.Set<TEntity>().Where(x => Ids.Contains(x.Id));

        await _context.RemoveRangeAsync(toRemoveList);
        await _context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Set<TEntity>()
                                   .Where(x => x.Id == id && !x.IsDeleted)
                                   .FirstOrDefaultAsync();

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TDto>> GetAllAsync()
    {
        return await _context.Set<TEntity>()
                             .AsNoTracking()
                             .Where(x => !x.IsDeleted)
                             .ProjectToType<TDto>()
                             .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<TDto> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>()
                             .AsNoTracking()
                             .Where(x => x.Id == id && !x.IsDeleted)
                             .ProjectToType<TDto>()
                             .FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TDto>> GetSearchListAsync(TQuery query, params string[] searchProps)
    {
        var filteredQuery = _context.Set<TEntity>().Where(x => !x.IsDeleted).ProjectToType<TDto>();

        if (query != null && !string.IsNullOrEmpty(query.SearchText) && filteredQuery.Any())
        {
            if (int.TryParse(query.SearchText, out int searchInteger))
            {
                filteredQuery = filteredQuery.Where(x => x.Id == searchInteger);
            }
            else
            {
                var properties = filteredQuery.FirstOrDefault().GetType().GetProperties();
                var parameter = Expression.Parameter(typeof(TDto), "x");
                Expression combinedExpression = null;

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(string) && searchProps.Contains(property.Name))
                    {
                        var propertyAccess = Expression.Property(parameter, property);
                        var searchTextExpression = Expression.Constant(query.SearchText);
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var containsExpression = Expression.Call(propertyAccess, containsMethod, searchTextExpression);

                        if (combinedExpression == null)
                        {
                            combinedExpression = containsExpression;
                        }
                        else
                        {
                            combinedExpression = Expression.Or(combinedExpression, containsExpression);
                        }
                    }
                }

                if (combinedExpression != null)
                {
                    var lambda = Expression.Lambda<Func<TDto, bool>>(combinedExpression, parameter);
                    filteredQuery = filteredQuery.Where(lambda);
                }
            }
        }

        int totalPages = (filteredQuery.Count() / query.PageSize) + 1;
        if (totalPages < query.PageNo)
            query.PageNo = totalPages;

        return await filteredQuery
            .OrderByDescending(x => x.Id)
            .Skip((query.PageNo - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id) != null;
    }

    /// <inheritdoc />
    public async Task<TDto> UpdateAsync(TDto dto)
    {
        var entity = await _context.Set<TEntity>().FindAsync(dto.Id);
        if (entity == null)
        {
            return null; // Or throw an exception, depending on your requirements.
        }

        dto.Adapt<TEntity>();
        await _context.SaveChangesAsync();

        return dto;
    }
}
