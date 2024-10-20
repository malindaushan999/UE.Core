using UE.Core.Common.DTOs;
using UE.Core.Common.Entities;

namespace UE.Core.Common.Interfaces;

/// <summary>
/// Represents a generic repository interface for CRUD operations on entities of type TEntity.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TDto">The DTO (Data Transfer Object) type.</typeparam>
public interface IGenericRepository<TEntity, TDto, TQuery>
    where TEntity : BaseEntity
    where TDto : BaseDto
    where TQuery : IPagination
{
    /// <summary>
    /// Asynchronously retrieves a list of entities of type TDto.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
    Task<IReadOnlyList<TDto>> GetAllAsync();

    /// <summary>
    /// Asynchronously retrieves an entity of type TDto by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved entity.</returns>
    Task<TDto> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronously gets a list of data transfer objects (DTOs) that match the given query and search properties.
    /// </summary>
    /// <param name="query">A query object that specifies the criteria for the search.</param>
    /// <param name="searchProps">An optional array of strings that specify the search properties to use.</param>
    /// <returns>A task that will eventually complete and return a list of DTOs.</returns>
    Task<IReadOnlyList<TDto>> GetSearchListAsync(TQuery query, params string[] searchProps);

    /// <summary>
    /// Asynchronously creates a new entity of type TEntity.
    /// </summary>
    /// <param name="dto">The DTO to create the entity from.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the created entity.</returns>
    Task<int> CreateAsync(TDto dto);

    /// <summary>
    /// Asynchronously updates an existing entity of type TEntity.
    /// </summary>
    /// <param name="dto">The DTO containing updated information for the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated DTO.</returns>
    Task<TDto> UpdateAsync(TDto dto);

    /// <summary>
    /// Asynchronously deletes an existing entity of type TEntity.
    /// </summary>
    /// <param name="dto">The DTO representing the entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates the success of the delete operation.</returns>
    Task<bool> DeleteAsync(int Id);

    /// <summary>
    /// Asynchronously deletes all items with the specified IDs.
    /// </summary>
    /// <param name="Id">A list of integers representing the IDs of the items to delete.</param>
    /// <returns>A task representing the asynchronous operation. Returns true if the items were successfully deleted; otherwise, false.</returns>
    Task<bool> DeleteAllAsync(List<int> Ids);

    /// <summary>
    /// Checks if an entity with the specified ID exists asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to check.</param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The task result contains true if the entity with the specified ID exists; otherwise, false.
    /// </returns>
    Task<bool> IsExistsAsync(int id);
}