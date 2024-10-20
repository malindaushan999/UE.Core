using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using UE.Core.Common.Repositories;
using UE.Core.Common.Tests.Mocks;

namespace UE.Core.Common.Tests.Repositories;

/// <summary>
/// Unit tests for the GenericRepository class, which provides common repository operations for entities.
/// </summary>
public class GenericRepositoryTests : IDisposable
{
    private readonly DbContextOptions<MockDbContext> _options;
    private readonly MockDbContext _dbContext;

    public GenericRepositoryTests()
    {
        // Arrange: Create a new DbContext with an in-memory database for testing.
        _options = new DbContextOptionsBuilder<MockDbContext>()
            .UseInMemoryDatabase(databaseName: "UE.Core.Common.UnitTest")
            .Options;

        _dbContext = new MockDbContext(_options);
    }

    public void Dispose()
    {
        var allEntities = _dbContext.Set<MockEntity>().ToList();
        foreach (var entity in allEntities)
        {
            _dbContext.Set<MockEntity>().Remove(entity);
        }
        _dbContext.SaveChanges();
    }

    [Fact]
    /// <summary>
    /// Tests the CreateAsync method of the GenericRepository.
    /// It should create an entity and verify its existence in the database.
    /// </summary>
    public async Task CreateAsync_ShouldCreateEntity()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var dto = new MockDto()
        {
            FirstName = "fName",
            LastName = "lName",
            Age = 20
        };

        // Act
        var createdId = await repository.CreateAsync(dto);

        // Assert
        var retrievedEntity = await _dbContext.Set<MockEntity>().FindAsync(createdId);
        retrievedEntity.Should().NotBeNull();
    }

    [Fact]
    /// <summary>
    /// Tests the GetByIdAsync method of the GenericRepository.
    /// It should retrieve an entity by its ID and verify its existence.
    /// </summary>
    public async Task GetByIdAsync_ShouldRetrieveEntity()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entity = new MockEntity()
        {
            FirstName = "fName",
            LastName = "lName",
            Age = 20
        };
        _dbContext.Set<MockEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrievedDto = await repository.GetByIdAsync(entity.Id);

        // Assert
        retrievedDto.Should().NotBeNull();
    }

    [Fact]
    /// <summary>
    /// Tests the GetByIdAsync method of the GenericRepository.
    /// It should not retrieve a deleted entity.
    /// </summary>
    public async Task GetByIdAsync_ShouldNotRetrieveDeletedEntity()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entity = new MockEntity()
        {
            FirstName = "fName",
            LastName = "lName",
            Age = 20
        };
        _dbContext.Set<MockEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();

        // Delete entity
        await repository.DeleteAsync(entity.Id);

        // Act
        var retrievedDto = await repository.GetByIdAsync(entity.Id);

        // Assert
        retrievedDto.Should().BeNull();
    }

    [Fact]
    /// <summary>
    /// Tests the UpdateAsync method of the GenericRepository.
    /// It should update an entity and verify the changes in the database.
    /// </summary>
    public async Task UpdateAsync_ShouldUpdateEntity()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entity = new MockEntity
        {
            FirstName = "fName",
            LastName = "lName",
            Age = 20
        };
        _dbContext.Set<MockEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();

        var updatedDto = new MockDto
        {
            Id = entity.Id,
            FirstName = "fNameUpdated",
            LastName = "lNameUpdated",
            Age = 25
        };

        // Act
        var updatedDtoResult = await repository.UpdateAsync(updatedDto);

        // Assert
        var updatedEntity = await _dbContext.Set<MockEntity>().FindAsync(updatedDto.Id);
        updatedEntity.Should().NotBeNull();
        updatedDtoResult.Should().NotBeNull();
        updatedDto.Should().Be(updatedDtoResult);
    }

    [Fact]
    /// <summary>
    /// Tests the DeleteAsync method of the GenericRepository.
    /// It should delete an entity and mark it as deleted in the database.
    /// </summary>
    public async Task DeleteAsync_ShouldDeleteEntity()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entity = new MockEntity
        {
            FirstName = "fName",
            LastName = "lName",
            Age = 20
        };
        _dbContext.Set<MockEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(entity.Id);

        // Assert
        var deletedEntity = await _dbContext.Set<MockEntity>().FindAsync(entity.Id);
        deletedEntity.Should().NotBeNull();
        deletedEntity!.IsDeleted.Should().BeTrue();
    }

    [Fact]
    /// <summary>
    /// Tests the GetAllAsync method of the GenericRepository.
    /// It should retrieve a list of entities and verify their existence.
    /// </summary>
    public async Task GetAllAsync_ShouldRetrieveListOfEntities()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entity1 = new MockEntity
        {
            FirstName = "fName_1",
            LastName = "lName_1",
            Age = 21
        };
        var entity2 = new MockEntity
        {
            FirstName = "fName_2",
            LastName = "lName_2",
            Age = 22
        };
        _dbContext.Set<MockEntity>().AddRange(entity1, entity2);
        await _dbContext.SaveChangesAsync();

        // Act
        var retrievedDtos = await repository.GetAllAsync();

        // Assert
        retrievedDtos.Should().NotBeNull();
        retrievedDtos.Should().HaveCount(2);
    }

    [Fact]
    /// <summary>
    /// Tests the GetAllAsync method of the GenericRepository.
    /// It should not retrieve deleted entities from the list.
    /// </summary>
    public async Task GetAllAsync_ShouldNotRetrieveDeletedListOfEntities()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entity1 = new MockEntity
        {
            FirstName = "fName_1",
            LastName = "lName_1",
            Age = 21
        };
        var entity2 = new MockEntity
        {
            FirstName = "fName_2",
            LastName = "lName_2",
            Age = 22
        };
        var entity3 = new MockEntity
        {
            FirstName = "fName_3",
            LastName = "lName_3",
            Age = 23
        };
        _dbContext.Set<MockEntity>().AddRange(entity1, entity2, entity3);
        await _dbContext.SaveChangesAsync();
        await repository.DeleteAsync(entity2.Id);

        // Act
        var retrievedDtos = await repository.GetAllAsync();

        // Assert
        retrievedDtos.Should().NotBeNull();
        retrievedDtos.Should().HaveCount(2);
        retrievedDtos.Should().NotContain(x => x.Equals(entity2.Adapt<MockDto>()));
    }

    [Fact]
    /// <summary>
    /// Deletes entities with valid IDs and verifies if they are removed from the database.
    /// </summary>
    public async Task DeleteAllAsync_ValidIds_ReturnsTrue()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);

        var entitiesToAdd = new List<MockEntity>
            {
                new() { Id = 1 },
                new() { Id = 2 },
                new() { Id = 3 },
            };

        await _dbContext.AddRangeAsync(entitiesToAdd);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.DeleteAllAsync(new List<int> { 1, 3 });

        // Assert
        result.Should().BeTrue();

        var remainingEntities = await repository.GetAllAsync();
        remainingEntities.Should().ContainSingle();
        remainingEntities.First().Id.Should().Be(2);
    }

    [Fact]
    /// <summary>
    /// Deletes entities with invalid IDs and verifies that they are not removed from the database.
    /// </summary>
    public async Task DeleteAllAsync_InvalidIds_ReturnsTrue()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);

        var entitiesToAdd = new List<MockEntity>
            {
                new() { Id = 1 },
                new() { Id = 2 },
                new() { Id = 3 },
            };

        await _dbContext.AddRangeAsync(entitiesToAdd);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.DeleteAllAsync(new List<int> { 4, 5 });

        // Assert
        result.Should().BeTrue();

        var remainingEntities = await _dbContext.Set<MockEntity>().ToListAsync();
        remainingEntities.Should().HaveCount(3);
    }

    [Fact()]
    public async Task GetSearchListAsync_ShouldRetrieveListOfEntities_FilteredBy_Id()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 5; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            SearchText = "2",
            PageNo = 1,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().ContainSingle();
        result.Single().Id.Should().Be(2);
    }

    [Fact()]
    public async Task GetSearchListAsync_ShouldRetrieveListOfEntities_FilteredBy_OneString()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 5; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            SearchText = "FName_3",
            PageNo = 1,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query, "FirstName");

        // Assert
        result.Should().NotBeNull();
        result.Should().ContainSingle();
        result.Single().FirstName.Should().Be("FName_3");
    }

    [Fact()]
    public async Task GetSearchListAsync_ShouldRetrieveListOfEntities_FilteredBy_TwoStrings()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 5; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }
        entityList.Add(new MockEntity { FirstName = $"FName_3", LastName = $"LName", Age = Random.Shared.Next(10, 35) });

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            SearchText = "Name_3",
            PageNo = 1,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query, "FirstName", "LastName");

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(x => x.FirstName.Contains("Name_3") || x.LastName.Contains("Name_3"));
    }

    [Fact]
    public async Task GetSearchListAsync_ShouldReturnFirstPage_If_InvalidPageNo()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 5; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            SearchText = "Name_3",
            PageNo = -1,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query, "FirstName");

        // Assert
        result.Should().NotBeNull();
        result.Should().ContainSingle();
        result.Single().FirstName.Should().Be("FName_3");
    }

    [Fact]
    public async Task GetSearchListAsync_ShouldReturnEmptyList_If_InvalidPageSize()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 10; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            SearchText = "Name_3",
            PageNo = -1,
            PageSize = -5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query, "FirstName");

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetSearchListAsync_ShouldReturnFirstPage_InDescendingOrder()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 15; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            PageNo = 1,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query);

        // Assert
        int[] expectedIds = { 11, 12, 13, 14, 15 };
        result.Should().NotBeNull();
        result.Should().HaveCount(5);
        result.Should().OnlyContain(x => expectedIds.Contains(x.Id));
        result.Should().BeInDescendingOrder(x => x.Id);
    }

    [Fact]
    public async Task GetSearchListAsync_ShouldReturnSecondPage_InDescendingOrder()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 15; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            PageNo = 2,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query);

        // Assert
        int[] expectedIds = { 6, 7, 8, 9, 10 };
        result.Should().NotBeNull();
        result.Should().HaveCount(5);
        result.Should().OnlyContain(x => expectedIds.Contains(x.Id));
        result.Should().BeInDescendingOrder(x => x.Id);
    }

    [Fact]
    public async Task GetSearchListAsync_ShouldReturnLastPage_InDescendingOrder()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 14; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            PageNo = 3,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query);

        // Assert
        int[] expectedIds = { 1, 2, 3, 4 };
        result.Should().NotBeNull();
        result.Should().HaveCount(4);
        result.Should().OnlyContain(x => expectedIds.Contains(x.Id));
        result.Should().BeInDescendingOrder(x => x.Id);
    }

    [Fact]
    public async Task GetSearchListAsync_ShouldReturnLastPage_InDescendingOrder_If_PageNoExceedsMaximum()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 14; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        MockQuery query = new()
        {
            PageNo = 5,
            PageSize = 5,
        };

        // Act
        var result = await repository.GetSearchListAsync(query);

        // Assert
        int[] expectedIds = { 1, 2, 3, 4 };
        result.Should().NotBeNull();
        result.Should().HaveCount(4);
        result.Should().OnlyContain(x => expectedIds.Contains(x.Id));
        result.Should().BeInDescendingOrder(x => x.Id);
    }

    [Fact]
    public async Task IsExistsAsync_ShouldReturnTrue_If_EntityExists()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 5; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.IsExistsAsync(1);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task IsExistsAsync_ShouldReturnFalse_If_EntityDoesNotExists()
    {
        // Arrange
        var repository = new GenericRepository<MockEntity, MockDbContext, MockDto, MockQuery>(_dbContext);
        var entityList = new List<MockEntity>();

        for (int i = 1; i <= 5; i++)
        {
            entityList.Add(new MockEntity { Id = i, FirstName = $"FName_{i}", LastName = $"LName_{i}", Age = Random.Shared.Next(10, 35) });
        }

        _dbContext.Set<MockEntity>().AddRange(entityList);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.IsExistsAsync(6);

        // Assert
        result.Should().BeFalse();
    }
}
